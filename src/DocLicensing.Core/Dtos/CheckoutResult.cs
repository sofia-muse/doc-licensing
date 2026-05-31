namespace DocLicensing.Core.Dtos;

public record CheckoutResult(
    int PurchaseId,
    string PaymentReference,
    decimal AmountPaid,
    string Currency,
    string DownloadUrl);
