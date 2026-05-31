namespace DocLicensing.Core.Dtos;

/// <summary>Catalog item already localised to a single requested language.</summary>
public record DocumentDto(
    int Id,
    string Code,
    string Title,
    string Description,
    decimal Price,
    string Currency,
    string Category);
