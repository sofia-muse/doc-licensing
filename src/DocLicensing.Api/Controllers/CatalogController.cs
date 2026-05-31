using DocLicensing.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DocLicensing.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly ICatalogService _catalog;

    public CatalogController(ICatalogService catalog) => _catalog = catalog;

    /// <summary>List licensable documents, localised by ?lang=he|en.</summary>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string lang = "en", CancellationToken ct = default)
        => Ok(await _catalog.GetCatalogAsync(lang, ct));

    /// <summary>Get a single document, localised by ?lang=he|en.</summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, [FromQuery] string lang = "en", CancellationToken ct = default)
    {
        var dto = await _catalog.GetByIdAsync(id, lang, ct);
        return dto is null ? NotFound() : Ok(dto);
    }
}
