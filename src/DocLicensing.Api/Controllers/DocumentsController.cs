using DocLicensing.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DocLicensing.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentRepository _repo;
    private readonly IPdfService _pdf;

    public DocumentsController(IDocumentRepository repo, IPdfService pdf)
    {
        _repo = repo;
        _pdf = pdf;
    }

    /// <summary>Download the watermarked, single-licence PDF for a completed purchase.</summary>
    [HttpGet("{id:int}/download")]
    public async Task<IActionResult> Download(int id, [FromQuery] int purchase, CancellationToken ct = default)
    {
        var p = await _repo.GetPurchaseAsync(purchase, ct);
        if (p is null || p.DocumentId != id)
            return NotFound(new { error = "Purchase not found for this document." });

        var doc = await _repo.GetByIdAsync(id, ct);
        if (doc is null) return NotFound();

        var bytes = _pdf.GenerateLicensedPdf(doc, p.BuyerName);
        return File(bytes, "application/pdf", $"{doc.Code}-licensed.pdf");
    }
}
