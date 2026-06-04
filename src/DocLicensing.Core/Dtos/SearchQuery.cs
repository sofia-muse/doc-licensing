namespace DocLicensing.Core.Dtos;

/// <summary>Search/filter/page query for the catalog. All fields optional except defaults.</summary>
public class SearchQuery
{
    /// <summary>Free-text fragment matched against code, title (EN/HE), description (EN/HE).</summary>
    public string? Q { get; set; }

    /// <summary>Exact category filter (case-insensitive).</summary>
    public string? Category { get; set; }

    /// <summary>Language used for the returned title/description.</summary>
    public string Lang { get; set; } = "en";

    /// <summary>1-based page number.</summary>
    public int Page { get; set; } = 1;

    /// <summary>Page size (1–100).</summary>
    public int PageSize { get; set; } = 20;
}
