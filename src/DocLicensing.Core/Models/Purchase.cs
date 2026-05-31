namespace DocLicensing.Core.Models;

/// <summary>A completed purchase that grants a single watermarked-PDF licence.</summary>
public class Purchase
{
    public int Id { get; set; }
    public int DocumentId { get; set; }
    public Document? Document { get; set; }
    public string BuyerName { get; set; } = "";
    public string BuyerEmail { get; set; } = "";
    public decimal AmountPaid { get; set; }
    public string Currency { get; set; } = "ILS";
    public string PaymentReference { get; set; } = "";
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}
