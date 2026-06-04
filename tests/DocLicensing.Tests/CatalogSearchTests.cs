using DocLicensing.Core.Abstractions;
using DocLicensing.Core.Dtos;
using DocLicensing.Core.Models;
using DocLicensing.Core.Services;
using Xunit;

namespace DocLicensing.Tests;

public class CatalogSearchTests
{
    [Fact]
    public async Task SearchAsync_returns_paged_result_localised_by_lang()
    {
        var sut = new CatalogService(new InMemoryRepo());

        var result = await sut.SearchAsync(new SearchQuery { Lang = "he", PageSize = 2 });

        Assert.Equal(3, result.Total);
        Assert.Equal(2, result.Items.Count);
        Assert.Equal(1, result.Page);
        // First item by Code-sort should be ISO-14001 with the Hebrew title:
        Assert.Equal("ISO-14001", result.Items[0].Code);
        Assert.Equal("ניהול סביבתי", result.Items[0].Title);
    }

    [Fact]
    public async Task SearchAsync_filters_by_q_and_category()
    {
        // The fake repo below honours `q` (substring across fields) and `category` (exact).
        var sut = new CatalogService(new InMemoryRepo());

        var byCode = await sut.SearchAsync(new SearchQuery { Q = "27001" });
        Assert.Single(byCode.Items);
        Assert.Equal("ISO-27001", byCode.Items[0].Code);

        var byCategory = await sut.SearchAsync(new SearchQuery { Category = "Environment" });
        Assert.Single(byCategory.Items);
        Assert.Equal("ISO-14001", byCategory.Items[0].Code);

        var miss = await sut.SearchAsync(new SearchQuery { Q = "nothing-matches-this" });
        Assert.Empty(miss.Items);
        Assert.Equal(0, miss.Total);
    }

    private sealed class InMemoryRepo : IDocumentRepository
    {
        private readonly List<Document> _docs = new()
        {
            new() { Id = 1, Code = "ISO-9001",  TitleEn = "Quality",      TitleHe = "איכות",   DescriptionEn = "Quality mgmt", DescriptionHe = "ניהול איכות", Category = "Management" },
            new() { Id = 2, Code = "ISO-14001", TitleEn = "Environment", TitleHe = "ניהול סביבתי", DescriptionEn = "Env mgmt",  DescriptionHe = "סביבתי",       Category = "Environment" },
            new() { Id = 3, Code = "ISO-27001", TitleEn = "Security",    TitleHe = "אבטחת מידע", DescriptionEn = "Sec mgmt",    DescriptionHe = "אבטחה",       Category = "Security" },
        };

        public Task<IReadOnlyList<Document>> GetAllAsync(CancellationToken ct = default) =>
            Task.FromResult<IReadOnlyList<Document>>(_docs);

        public Task<Document?> GetByIdAsync(int id, CancellationToken ct = default) =>
            Task.FromResult(_docs.FirstOrDefault(d => d.Id == id));

        public Task<Purchase> AddPurchaseAsync(Purchase purchase, CancellationToken ct = default) =>
            throw new NotSupportedException();

        public Task<Purchase?> GetPurchaseAsync(int id, CancellationToken ct = default) =>
            Task.FromResult<Purchase?>(null);

        public Task<(IReadOnlyList<Document> Items, int Total)> SearchAsync(string? q, string? category, int page, int pageSize, CancellationToken ct = default)
        {
            IEnumerable<Document> query = _docs;

            if (!string.IsNullOrWhiteSpace(q))
            {
                var needle = q.Trim();
                query = query.Where(d =>
                    d.Code.Contains(needle, StringComparison.OrdinalIgnoreCase) ||
                    d.TitleEn.Contains(needle, StringComparison.OrdinalIgnoreCase) ||
                    d.TitleHe.Contains(needle, StringComparison.OrdinalIgnoreCase) ||
                    d.DescriptionEn.Contains(needle, StringComparison.OrdinalIgnoreCase) ||
                    d.DescriptionHe.Contains(needle, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(d => d.Category == category);

            var ordered = query.OrderBy(d => d.Code).ToList();
            var items = ordered.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return Task.FromResult<(IReadOnlyList<Document>, int)>((items, ordered.Count));
        }
    }
}
