using DocLicensing.Core.Models;

namespace DocLicensing.Infrastructure.Data;

/// <summary>Seed catalog (EF Core HasData). Bilingual EN/HE titles + descriptions.</summary>
public static class SeedData
{
    public static readonly Document[] Documents =
    {
        new()
        {
            Id = 1, Code = "ISO-9001",
            TitleEn = "Quality Management Systems", TitleHe = "מערכות ניהול איכות",
            DescriptionEn = "Requirements for a quality management system.",
            DescriptionHe = "דרישות למערכת ניהול איכות.",
            Price = 129.00m, Currency = "ILS", Category = "Management"
        },
        new()
        {
            Id = 2, Code = "ISO-27001",
            TitleEn = "Information Security Management", TitleHe = "ניהול אבטחת מידע",
            DescriptionEn = "Requirements for information security controls.",
            DescriptionHe = "דרישות לבקרות אבטחת מידע.",
            Price = 159.00m, Currency = "ILS", Category = "Security"
        },
        new()
        {
            Id = 3, Code = "ISO-14001",
            TitleEn = "Environmental Management", TitleHe = "ניהול סביבתי",
            DescriptionEn = "Requirements for an environmental management system.",
            DescriptionHe = "דרישות למערכת ניהול סביבתי.",
            Price = 119.00m, Currency = "ILS", Category = "Environment"
        },
        new()
        {
            Id = 4, Code = "ISO-45001",
            TitleEn = "Occupational Health & Safety", TitleHe = "בריאות ובטיחות תעסוקתית",
            DescriptionEn = "Requirements for occupational health and safety management.",
            DescriptionHe = "דרישות לניהול בריאות ובטיחות תעסוקתית.",
            Price = 139.00m, Currency = "ILS", Category = "Safety"
        }
    };
}
