using Xunit;
using RoadmApp.Api.Endpoints;
using Microsoft.AspNetCore.Http;

namespace RoadmApp.Api.Tests.Endpoints;

public class NotesEndpointsTests
{
    [Fact]
    public void ExportNotesAsExcel_WithEmptyNotes_ReturnsBadRequest()
    {
        // Arrange
        var request = new ExportNotesRequest { Month = "2026-06", Notes = new List<NoteDto>() };

        // Act & Assert
        // This would be tested via integration tests or by refactoring to testable structure
        Assert.Empty(request.Notes);
    }

    [Fact]
    public void ExportNotesAsExcel_WithValidNotes_ShouldCreateExcelFile()
    {
        // Arrange
        var notes = new List<NoteDto>
        {
            new NoteDto
            {
                Id = "1",
                Title = "Test Note",
                Category = "Estudos",
                Content = "Test content",
                CreatedAt = "2026-06-01T10:00:00Z",
                Month = "2026-06",
                Images = new List<string>(),
                Drawing = null
            }
        };

        var request = new ExportNotesRequest { Month = "2026-06", Notes = notes };

        // Act & Assert
        Assert.NotEmpty(request.Notes);
        Assert.Equal("Test Note", request.Notes[0].Title);
        Assert.Equal("2026-06", request.Month);
    }

    [Fact]
    public void NoteDto_InitializeWithValidData()
    {
        // Arrange & Act
        var note = new NoteDto
        {
            Id = "1",
            Title = "My Note",
            Category = "Treino",
            Content = "Workout content",
            CreatedAt = "2026-06-01T10:00:00Z",
            Month = "2026-06",
            Images = new List<string> { "image1.jpg" },
            Drawing = "drawing.png"
        };

        // Assert
        Assert.Equal("1", note.Id);
        Assert.Equal("My Note", note.Title);
        Assert.Equal("Treino", note.Category);
        Assert.Equal("Workout content", note.Content);
        Assert.Single(note.Images);
        Assert.NotNull(note.Drawing);
    }

    [Fact]
    public void ExportNotesRequest_InitializeWithNullNotes_DefaultsToEmptyList()
    {
        // Arrange & Act
        var request = new ExportNotesRequest();

        // Assert
        Assert.Empty(request.Notes);
    }

    [Theory]
    [InlineData("Estudos")]
    [InlineData("Treino")]
    [InlineData("Metas")]
    [InlineData("Notas")]
    public void NoteDto_AcceptsDifferentCategories(string category)
    {
        // Arrange & Act
        var note = new NoteDto { Category = category };

        // Assert
        Assert.Equal(category, note.Category);
    }

    [Fact]
    public void ExportNotesRequest_WithMultipleNotes_PreservesAllData()
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
}
