using DocLicensing.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DocLicensing.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Document> Documents => Set<Document>();
    public DbSet<Purchase> Purchases => Set<Purchase>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Document>(e =>
        {
            e.Property(p => p.Price).HasColumnType("decimal(18,2)");
            e.HasIndex(p => p.Code).IsUnique();
        });

        b.Entity<Purchase>(e =>
        {
            e.Property(p => p.AmountPaid).HasColumnType("decimal(18,2)");
            e.HasOne(p => p.Document).WithMany().HasForeignKey(p => p.DocumentId);
        });

        b.Entity<Document>().HasData(SeedData.Documents);
    }
}
