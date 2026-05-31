using DocLicensing.Core.Dtos;

namespace DocLicensing.Core.Abstractions;

public interface ICatalogService
{
    Task<IReadOnlyList<DocumentDto>> GetCatalogAsync(string lang, CancellationToken ct = default);
    Task<DocumentDto?> GetByIdAsync(int id, string lang, CancellationToken ct = default);
}
