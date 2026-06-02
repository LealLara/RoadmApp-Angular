using Xunit;
using RoadmApp.Api.Endpoints;

namespace RoadmApp.Api.Tests.Domain;

public class ExportNotesRequestTests
{
    [Fact]
    public void ExportNotesRequest_Initialize_WithDefaultValues()
    {
        // Arrange & Act
        var request = new ExportNotesRequest();

        // Assert
        Assert.Empty(request.Month);
        Assert.Empty(request.Notes);
    }

    [Fact]
    public void ExportNotesRequest_SetMonth_AndNotes()
    {
        // Arrange
        var request = new ExportNotesRequest();
        var notes = new List<NoteDto>
        {
            new NoteDto { Id = "1", Title = "Test" }
        };

        // Act
        request.Month = "2026-06";
        request.Notes = notes;

        // Assert
        Assert.Equal("2026-06", request.Month);
        Assert.Single(request.Notes);
    }
}

public class NoteDtoTests
{
    [Fact]
    public void NoteDto_Initialize_WithDefaultValues()
    {
        // Arrange & Act
        var note = new NoteDto();

        // Assert
        Assert.Empty(note.Id);
        Assert.Empty(note.Title);
        Assert.Empty(note.Category);
        Assert.Empty(note.Content);
        Assert.Empty(note.CreatedAt);
        Assert.Empty(note.Month);
        Assert.Empty(note.Images);
        Assert.Null(note.Drawing);
    }

    [Fact]
    public void NoteDto_SetProperties_AndRetrieve()
    {
        // Arrange
        var note = new NoteDto
        {
            Id = "1",
            Title = "My Note",
            Category = "Estudos",
            Content = "Test content",
            CreatedAt = "2026-06-01T10:00:00Z",
            Month = "2026-06",
            Images = new List<string> { "img1.jpg" },
            Drawing = "drawing.png"
        };

        // Act & Assert
        Assert.Equal("1", note.Id);
        Assert.Equal("My Note", note.Title);
        Assert.Equal("Estudos", note.Category);
        Assert.Equal("Test content", note.Content);
        Assert.Single(note.Images);
        Assert.NotNull(note.Drawing);
    }

    [Theory]
    [InlineData("Estudos")]
    [InlineData("Treino")]
    [InlineData("Metas")]
    [InlineData("Notas")]
    [InlineData("Prioridades")]
    [InlineData("Blocos de estudo")]
    [InlineData("Ideias")]
    [InlineData("Hábitos")]
    [InlineData("Gastos")]
    [InlineData("Pequenas vitórias")]
    public void NoteDto_AcceptValidCategories(string category)
    {
        // Arrange & Act
        var note = new NoteDto { Category = category };

        // Assert
        Assert.Equal(category, note.Category);
    }

    [Fact]
    public void NoteDto_WithMultipleImages()
    {
        // Arrange
        var images = new List<string> { "img1.jpg", "img2.png", "img3.gif" };
        var note = new NoteDto { Images = images };

        // Act & Assert
        Assert.Equal(3, note.Images.Count);
        Assert.Contains("img1.jpg", note.Images);
        Assert.Contains("img2.png", note.Images);
        Assert.Contains("img3.gif", note.Images);
    }

    [Fact]
    public void NoteDto_WithDrawing()
    {
        // Arrange
        var drawing = "data:image/png;base64,...";
        var note = new NoteDto { Drawing = drawing };

        // Act & Assert
        Assert.NotNull(note.Drawing);
        Assert.StartsWith("data:image", note.Drawing);
    }
}

public class ExportListTests
{
    [Fact]
    public void ExportNotesList_WithMultipleNotes_PreservesData()
    {
        // Arrange
        var notes = new List<NoteDto>
        {
            new NoteDto { Id = "1", Title = "Note 1", Category = "Estudos" },
            new NoteDto { Id = "2", Title = "Note 2", Category = "Treino" },
            new NoteDto { Id = "3", Title = "Note 3", Category = "Metas" }
        };
        var request = new ExportNotesRequest { Month = "2026-06", Notes = notes };

        // Act & Assert
        Assert.Equal(3, request.Notes.Count);
        Assert.Equal("Note 1", request.Notes[0].Title);
        Assert.Equal("Note 2", request.Notes[1].Title);
        Assert.Equal("Note 3", request.Notes[2].Title);
    }

    [Fact]
    public void ExportNotesList_FilterByMonth()
    {
        // Arrange
        var notes = new List<NoteDto>
        {
            new NoteDto { Id = "1", Month = "2026-06" },
            new NoteDto { Id = "2", Month = "2026-05" },
            new NoteDto { Id = "3", Month = "2026-06" }
        };

        // Act
        var filtered = notes.Where(n => n.Month == "2026-06").ToList();

        // Assert
        Assert.Equal(2, filtered.Count);
        Assert.All(filtered, n => Assert.Equal("2026-06", n.Month));
    }
}
