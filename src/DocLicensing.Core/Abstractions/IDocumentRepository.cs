using DocLicensing.Core.Dtos;
using DocLicensing.Core.Models;

namespace DocLicensing.Core.Abstractions;

public interface IDocumentRepository
{
    Task<IReadOnlyList<Document>> GetAllAsync(CancellationToken ct = default);
    Task<Document?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Purchase> AddPurchaseAsync(Purchase purchase, CancellationToken ct = default);
    Task<Purchase?> GetPurchaseAsync(int id, CancellationToken ct = default);

    /// <summary>Free-text search across code, titles and descriptions, with category filter + paging.</summary>
    Task<(IReadOnlyList<Document> Items, int Total)> SearchAsync(string? q, string? category, int page, int pageSize, CancellationToken ct = default);
}
