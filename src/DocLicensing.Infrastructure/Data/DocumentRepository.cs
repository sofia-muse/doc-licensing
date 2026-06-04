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

    public async Task<(IReadOnlyList<Document> Items, int Total)> SearchAsync(
        string? q, string? category, int page, int pageSize, CancellationToken ct = default)
    {
        var query = _db.Documents.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            var pattern = $"%{q.Trim()}%";
            query = query.Where(d =>
                EF.Functions.Like(d.Code, pattern) ||
                EF.Functions.Like(d.TitleEn, pattern) ||
                EF.Functions.Like(d.TitleHe, pattern) ||
                EF.Functions.Like(d.DescriptionEn, pattern) ||
                EF.Functions.Like(d.DescriptionHe, pattern));
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            var cat = category.Trim();
            query = query.Where(d => d.Category == cat);
        }

        var total = await query.CountAsync(ct);

        var items = await query
            .OrderBy(d => d.Code)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return (items, total);
    }
}
