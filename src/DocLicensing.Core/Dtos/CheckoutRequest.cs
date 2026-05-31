namespace DocLicensing.Core.Dtos;

public class CheckoutRequest
{
    public int DocumentId { get; set; }
    public string BuyerName { get; set; } = "";
    public string BuyerEmail { get; set; } = "";
}
