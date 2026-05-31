using DocLicensing.Core.Abstractions;
using DocLicensing.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DocLicensing.Infrastructure.Data;

public class DocumentRepository : IDocumentRepository
{
    private readonly AppDbContext _db;

    public DocumentRepository(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<Document>> GetAllAsync(CancellationToken ct = default) =>
        await _db.Documents.AsNoTracking().OrderBy(d => d.Code).ToListAsync(ct);

    public Task<Document?> GetByIdAsync(int id, CancellationToken ct = default) =>
        _db.Documents.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id, ct);

    public async Task<Purchase> AddPurchaseAsync(Purchase purchase, CancellationToken ct = default)
    {
        _db.Purchases.Add(purchase);
        await _db.SaveChangesAsync(ct);
        return purchase;
    }

    public Task<Purchase?> GetPurchaseAsync(int id, CancellationToken ct = default) =>
        _db.Purchases.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, ct);
}
