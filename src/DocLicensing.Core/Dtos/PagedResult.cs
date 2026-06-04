namespace DocLicensing.Core.Dtos;

/// <summary>A page of items together with the total count and the page coordinates.</summary>
public record PagedResult<T>(IReadOnlyList<T> Items, int Total, int Page, int PageSize);
