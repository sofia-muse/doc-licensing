using DocLicensing.Core.Abstractions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using Doc = DocLicensing.Core.Models.Document;

namespace DocLicensing.Infrastructure.Pdf;

/// <summary>Generates a watermarked, single-licence PDF for a purchased document.</summary>
public class QuestPdfWatermarkService : IPdfService
{
    public byte[] GenerateLicensedPdf(Doc document, string licensedTo)
    {
        return QuestPDF.Fluent.Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.DefaultTextStyle(t => t.FontSize(12));

                page.Header()
                    .Text($"{document.Code} — {document.TitleEn}")
                    .SemiBold().FontSize(18);

                page.Content().Column(col =>
                {
                    col.Spacing(10);
                    col.Item().Text(document.DescriptionEn);
                    col.Item().Text($"Category: {document.Category}");
                    col.Item().Text($"Price: {document.Price:0.00} {document.Currency}");

                    col.Item().PaddingTop(60)
                        .Text($"LICENSED TO: {licensedTo}")
                        .FontColor(Colors.Grey.Lighten1).FontSize(28).Bold();

                    col.Item()
                        .Text("This is a watermarked, single-licence copy. Redistribution is not permitted.")
                        .FontColor(Colors.Grey.Medium);
                });

                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("DocLicensing demo • ");
                    x.Span($"Issued {DateTime.UtcNow:yyyy-MM-dd}");
                });
            });
        }).GeneratePdf();
    }
}
