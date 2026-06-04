using DocLicensing.Core.Abstractions;
using DocLicensing.Core.Dtos;
using DocLicensing.Core.Models;

namespace DocLicensing.Core.Services;

public class CatalogService : ICatalogService
{
    private readonly IDocumentRepository _repo;

    public CatalogService(IDocumentRepository repo) => _repo = repo;

    public async Task<IReadOnlyList<DocumentDto>> GetCatalogAsync(string lang, CancellationToken ct = default)
    {
        var docs = await _repo.GetAllAsync(ct);
        return docs.Select(d => Map(d, lang)).ToList();
    }

    public async Task<DocumentDto?> GetByIdAsync(int id, string lang, CancellationToken ct = default)
    {
        var doc = await _repo.GetByIdAsync(id, ct);
        return doc is null ? null : Map(doc, lang);
    }

    public async Task<PagedResult<DocumentDto>> SearchAsync(SearchQuery query, CancellationToken ct = default)
    {
        var (items, total) = await _repo.SearchAsync(query.Q, query.Category, query.Page, query.PageSize, ct);
        var dtos = items.Select(d => Map(d, query.Lang)).ToList();
        return new PagedResult<DocumentDto>(dtos, total, query.Page, query.PageSize);
    }

    private static DocumentDto Map(Document d, string lang)
    {
        var he = string.Equals(lang, "he", StringComparison.OrdinalIgnoreCase);
        return new DocumentDto(
            d.Id,
            d.Code,
            he ? d.TitleHe : d.TitleEn,
            he ? d.DescriptionHe : d.DescriptionEn,
            d.Price,
            d.Currency,
            d.Category);
    }
}
