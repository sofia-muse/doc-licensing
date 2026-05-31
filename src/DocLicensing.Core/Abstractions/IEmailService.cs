using DocLicensing.Core.Models;

namespace DocLicensing.Core.Abstractions;

public interface IEmailService
{
    Task SendReceiptAsync(string toEmail, Document document, PaymentResult payment, CancellationToken ct = default);
}
