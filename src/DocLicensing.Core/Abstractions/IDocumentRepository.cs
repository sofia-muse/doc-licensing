using DocLicensing.Core.Models;

namespace DocLicensing.Core.Abstractions;

public interface IDocumentRepository
{
    Task<IReadOnlyList<Document>> GetAllAsync(CancellationToken ct = default);
    Task<Document?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Purchase> AddPurchaseAsync(Purchase purchase, CancellationToken ct = default);
    Task<Purchase?> GetPurchaseAsync(int id, CancellationToken ct = default);
}
