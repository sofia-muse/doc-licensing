using DocLicensing.Core.Models;

namespace DocLicensing.Infrastructure.Data;

/// <summary>Seed catalog (EF Core HasData). Bilingual EN/HE titles + descriptions, 12 categories.</summary>
public static class SeedData
{
    public static readonly Document[] Documents =
    {
        new()
        {
            Id = 1, Code = "ISO-9001",
            TitleEn = "Quality Management Systems", TitleHe = "מערכות ניהול איכות",
            DescriptionEn = "Requirements for a quality management system any organization can apply.",
            DescriptionHe = "דרישות למערכת ניהול איכות שיכולה לחול על כל ארגון.",
            Price = 129.00m, Currency = "ILS", Category = "Management"
        },
        new()
        {
            Id = 2, Code = "ISO-27001",
            TitleEn = "Information Security Management", TitleHe = "מערכות ניהול אבטחת מידע",
            DescriptionEn = "Requirements for establishing, maintaining and continually improving information security.",
            DescriptionHe = "דרישות להקמה, תחזוקה ושיפור מתמשך של אבטחת מידע.",
            Price = 159.00m, Currency = "ILS", Category = "Security"
        },
        new()
        {
            Id = 3, Code = "ISO-14001",
            TitleEn = "Environmental Management Systems", TitleHe = "מערכות ניהול סביבתי",
            DescriptionEn = "Requirements for an environmental management system that improves environmental performance.",
            DescriptionHe = "דרישות למערכת ניהול סביבתי המשפרת ביצועים סביבתיים.",
            Price = 119.00m, Currency = "ILS", Category = "Environment"
        },
        new()
        {
            Id = 4, Code = "ISO-45001",
            TitleEn = "Occupational Health and Safety Management", TitleHe = "מערכות ניהול בריאות ובטיחות תעסוקתית",
            DescriptionEn = "Requirements for occupational health and safety management systems.",
            DescriptionHe = "דרישות למערכות ניהול בריאות ובטיחות תעסוקתית.",
            Price = 139.00m, Currency = "ILS", Category = "Safety"
        },
        new()
        {
            Id = 5, Code = "ISO-22000",
            TitleEn = "Food Safety Management Systems", TitleHe = "מערכות ניהול בטיחות מזון",
            DescriptionEn = "Requirements for any organization in the food chain to demonstrate food safety control.",
            DescriptionHe = "דרישות לכל ארגון בשרשרת המזון להוכיח בקרת בטיחות מזון.",
            Price = 149.00m, Currency = "ILS", Category = "Food"
        },
        new()
        {
            Id = 6, Code = "ISO-13485",
            TitleEn = "Medical Devices — Quality Management", TitleHe = "מכשור רפואי — ניהול איכות",
            DescriptionEn = "Quality management system requirements for the medical devices industry.",
            DescriptionHe = "דרישות מערכת ניהול איכות עבור תעשיית המכשור הרפואי.",
            Price = 179.00m, Currency = "ILS", Category = "Medical"
        },
        new()
        {
            Id = 7, Code = "ISO-50001",
            TitleEn = "Energy Management Systems", TitleHe = "מערכות ניהול אנרגיה",
            DescriptionEn = "Framework for managing energy performance, including efficiency, use and consumption.",
            DescriptionHe = "מסגרת לניהול ביצועי אנרגיה, כולל יעילות, שימוש וצריכה.",
            Price = 129.00m, Currency = "ILS", Category = "Energy"
        },
        new()
        {
            Id = 8, Code = "ISO-37001",
            TitleEn = "Anti-bribery Management Systems", TitleHe = "מערכות ניהול נגד שוחד",
            DescriptionEn = "Requirements and guidance for establishing an anti-bribery management system.",
            DescriptionHe = "דרישות והנחיות להקמת מערכת ניהול למניעת שוחד.",
            Price = 139.00m, Currency = "ILS", Category = "Compliance"
        },
        new()
        {
            Id = 9, Code = "ISO-31000",
            TitleEn = "Risk Management Guidelines", TitleHe = "הנחיות לניהול סיכונים",
            DescriptionEn = "Principles, framework and process for managing risk faced by any organization.",
            DescriptionHe = "עקרונות, מסגרת ותהליך לניהול סיכונים העומדים בפני כל ארגון.",
            Price = 109.00m, Currency = "ILS", Category = "Risk"
        },
        new()
        {
            Id = 10, Code = "ISO-20000-1",
            TitleEn = "IT Service Management — Requirements", TitleHe = "ניהול שירותי IT — דרישות",
            DescriptionEn = "Requirements for a service management system delivering value to customers.",
            DescriptionHe = "דרישות למערכת ניהול שירות המספקת ערך ללקוחות.",
            Price = 169.00m, Currency = "ILS", Category = "IT"
        },
        new()
        {
            Id = 11, Code = "ISO-21500",
            TitleEn = "Project, Programme and Portfolio Management — Guidance", TitleHe = "הנחיות לניהול פרויקטים, תוכניות ותיקים",
            DescriptionEn = "High-level guidance on concepts and processes of project management.",
            DescriptionHe = "הנחיות ברמה גבוהה למושגים ולתהליכים של ניהול פרויקטים.",
            Price = 119.00m, Currency = "ILS", Category = "Project Management"
        },
        new()
        {
            Id = 12, Code = "ISO-19011",
            TitleEn = "Guidelines for Auditing Management Systems", TitleHe = "קווים מנחים לביקורת מערכות ניהול",
            DescriptionEn = "Guidance on auditing management systems, including audit principles and managing programmes.",
            DescriptionHe = "הנחיות לביקורת מערכות ניהול, כולל עקרונות ביקורת וניהול תוכניות.",
            Price = 99.00m, Currency = "ILS", Category = "Audit"
        }
    };
}
