using DocLicensing.Core.Abstractions;
using DocLicensing.Core.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace DocLicensing.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly ICatalogService _catalog;
    private readonly IValidator<SearchQuery> _searchValidator;

    public CatalogController(ICatalogService catalog, IValidator<SearchQuery> searchValidator)
    {
        _catalog = catalog;
        _searchValidator = searchValidator;
    }

    /// <summary>List licensable documents, localised by ?lang=he|en.</summary>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string lang = "en", CancellationToken ct = default)
        => Ok(await _catalog.GetCatalogAsync(lang, ct));

    /// <summary>
    /// Search the catalog with optional ?q=, ?category=, ?lang=, ?page=, ?pageSize=.
    /// Returns a paged result with the total match count.
    /// </summary>
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SearchQuery query, CancellationToken ct = default)
    {
        var validation = await _searchValidator.ValidateAsync(query, ct);
        if (!validation.IsValid)
            return ValidationProblem(new ValidationProblemDetails(validation.ToDictionary()));

        return Ok(await _catalog.SearchAsync(query, ct));
    }

    /// <summary>Get a single document, localised by ?lang=he|en.</summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, [FromQuery] string lang = "en", CancellationToken ct = default)
    {
        var dto = await _catalog.GetByIdAsync(id, lang, ct);
        return dto is null ? NotFound() : Ok(dto);
    }
}
