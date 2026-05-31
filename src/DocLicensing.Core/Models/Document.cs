namespace DocLicensing.Core.Models;

/// <summary>A licensable standards document, with title/description in both languages.</summary>
public class Document
{
    public int Id { get; set; }
    public string Code { get; set; } = "";
    public string TitleEn { get; set; } = "";
    public string TitleHe { get; set; } = "";
    public string DescriptionEn { get; set; } = "";
    public string DescriptionHe { get; set; } = "";
    public decimal Price { get; set; }
    public string Currency { get; set; } = "ILS";
    public string Category { get; set; } = "";
}
