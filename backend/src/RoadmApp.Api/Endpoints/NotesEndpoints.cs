using ClosedXML.Excel;

namespace RoadmApp.Api.Endpoints;

public static class NotesEndpoints
{
    public static void MapNotesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/notes")
            .WithName("Notes")
            .WithOpenApi();

        group.MapPost("/export-excel", ExportNotesAsExcel)
            .WithName("Export Notes to Excel")
            .WithDescription("Export notes to an Excel file (.xlsx)");
    }

    private static IResult ExportNotesAsExcel(ExportNotesRequest request)
    {
        try
        {
            if (request.Notes == null || request.Notes.Count == 0)
            {
                return Results.BadRequest(new { error = "Nenhuma anotação para exportar." });
            }

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Anotações");

            // Cabeçalhos
            var headerRow = worksheet.Row(1);
            headerRow.Cell(1).Value = "Data";
            headerRow.Cell(2).Value = "Categoria";
            headerRow.Cell(3).Value = "Título";
            headerRow.Cell(4).Value = "Conteúdo";
            headerRow.Cell(5).Value = "Quantidade de Imagens";
            headerRow.Cell(6).Value = "Possui Desenho";

            // Estilar cabeçalho
            var headerStyle = headerRow.Style;
            headerStyle.Font.Bold = true;
            headerStyle.Font.FontColor = XLColor.White;
            headerStyle.Fill.BackgroundColor = XLColor.FromArgb(0x0f766e);
            headerStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            headerStyle.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            // Dados
            int rowNumber = 2;
            foreach (var note in request.Notes)
            {
                var row = worksheet.Row(rowNumber);
                row.Cell(1).Value = note.CreatedAt.Split('T')[0];
                row.Cell(2).Value = note.Category;
                row.Cell(3).Value = note.Title;
                row.Cell(4).Value = note.Content;
                row.Cell(5).Value = note.Images.Count;
                row.Cell(6).Value = note.Drawing != null ? "SIM" : "NÃO";

                // Quebra de linha automática para células longas
                row.Cell(4).Style.Alignment.WrapText = true;

                rowNumber++;
            }

            // Ajustar largura das colunas
            worksheet.Column(1).Width = 12;
            worksheet.Column(2).Width = 18;
            worksheet.Column(3).Width = 25;
            worksheet.Column(4).Width = 50;
            worksheet.Column(5).Width = 15;
            worksheet.Column(6).Width = 15;

            // Gerar arquivo
            using var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            memoryStream.Position = 0;

            var fileName = $"roadmapp-notas-{request.Month}.xlsx";
            return Results.File(
                memoryStream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName
            );
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { error = $"Erro ao gerar arquivo: {ex.Message}" });
        }
    }
}

public class ExportNotesRequest
{
    public string Month { get; set; } = string.Empty;
    public List<NoteDto> Notes { get; set; } = new();
}

public class NoteDto
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
    public string Month { get; set; } = string.Empty;
    public List<string> Images { get; set; } = new();
    public string? Drawing { get; set; }
}
