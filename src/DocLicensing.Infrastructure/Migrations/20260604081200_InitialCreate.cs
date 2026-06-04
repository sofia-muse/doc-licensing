using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DocLicensing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleHe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionHe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    BuyerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchases_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "Category", "Code", "Currency", "DescriptionEn", "DescriptionHe", "Price", "TitleEn", "TitleHe" },
                values: new object[,]
                {
                    { 1, "Management", "ISO-9001", "ILS", "Requirements for a quality management system any organization can apply.", "דרישות למערכת ניהול איכות שיכולה לחול על כל ארגון.", 129.00m, "Quality Management Systems", "מערכות ניהול איכות" },
                    { 2, "Security", "ISO-27001", "ILS", "Requirements for establishing, maintaining and continually improving information security.", "דרישות להקמה, תחזוקה ושיפור מתמשך של אבטחת מידע.", 159.00m, "Information Security Management", "מערכות ניהול אבטחת מידע" },
                    { 3, "Environment", "ISO-14001", "ILS", "Requirements for an environmental management system that improves environmental performance.", "דרישות למערכת ניהול סביבתי המשפרת ביצועים סביבתיים.", 119.00m, "Environmental Management Systems", "מערכות ניהול סביבתי" },
                    { 4, "Safety", "ISO-45001", "ILS", "Requirements for occupational health and safety management systems.", "דרישות למערכות ניהול בריאות ובטיחות תעסוקתית.", 139.00m, "Occupational Health and Safety Management", "מערכות ניהול בריאות ובטיחות תעסוקתית" },
                    { 5, "Food", "ISO-22000", "ILS", "Requirements for any organization in the food chain to demonstrate food safety control.", "דרישות לכל ארגון בשרשרת המזון להוכיח בקרת בטיחות מזון.", 149.00m, "Food Safety Management Systems", "מערכות ניהול בטיחות מזון" },
                    { 6, "Medical", "ISO-13485", "ILS", "Quality management system requirements for the medical devices industry.", "דרישות מערכת ניהול איכות עבור תעשיית המכשור הרפואי.", 179.00m, "Medical Devices — Quality Management", "מכשור רפואי — ניהול איכות" },
                    { 7, "Energy", "ISO-50001", "ILS", "Framework for managing energy performance, including efficiency, use and consumption.", "מסגרת לניהול ביצועי אנרגיה, כולל יעילות, שימוש וצריכה.", 129.00m, "Energy Management Systems", "מערכות ניהול אנרגיה" },
                    { 8, "Compliance", "ISO-37001", "ILS", "Requirements and guidance for establishing an anti-bribery management system.", "דרישות והנחיות להקמת מערכת ניהול למניעת שוחד.", 139.00m, "Anti-bribery Management Systems", "מערכות ניהול נגד שוחד" },
                    { 9, "Risk", "ISO-31000", "ILS", "Principles, framework and process for managing risk faced by any organization.", "עקרונות, מסגרת ותהליך לניהול סיכונים העומדים בפני כל ארגון.", 109.00m, "Risk Management Guidelines", "הנחיות לניהול סיכונים" },
                    { 10, "IT", "ISO-20000-1", "ILS", "Requirements for a service management system delivering value to customers.", "דרישות למערכת ניהול שירות המספקת ערך ללקוחות.", 169.00m, "IT Service Management — Requirements", "ניהול שירותי IT — דרישות" },
                    { 11, "Project Management", "ISO-21500", "ILS", "High-level guidance on concepts and processes of project management.", "הנחיות ברמה גבוהה למושגים ולתהליכים של ניהול פרויקטים.", 119.00m, "Project, Programme and Portfolio Management — Guidance", "הנחיות לניהול פרויקטים, תוכניות ותיקים" },
                    { 12, "Audit", "ISO-19011", "ILS", "Guidance on auditing management systems, including audit principles and managing programmes.", "הנחיות לביקורת מערכות ניהול, כולל עקרונות ביקורת וניהול תוכניות.", 99.00m, "Guidelines for Auditing Management Systems", "קווים מנחים לביקורת מערכות ניהול" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_Code",
                table: "Documents",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_DocumentId",
                table: "Purchases",
                column: "DocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Documents");
        }
    }
}
