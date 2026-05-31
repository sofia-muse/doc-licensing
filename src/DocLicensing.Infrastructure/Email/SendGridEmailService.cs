using DocLicensing.Core.Abstractions;
using DocLicensing.Core.Models;
using Microsoft.Extensions.Configuration;

namespace DocLicensing.Infrastructure.Email;

/// <summary>
/// Skeleton SendGrid integration. Wire the SendGrid client here.
/// Reads Email:SendGrid:ApiKey from configuration / user-secrets. See README "Day 3".
/// </summary>
public class SendGridEmailService : IEmailService
{
    private readonly string? _apiKey;

    public SendGridEmailService(IConfiguration config) => _apiKey = config["Email:SendGrid:ApiKey"];

    public Task SendReceiptAsync(string toEmail, Document document, PaymentResult payment, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(_apiKey))
            throw new InvalidOperationException("Email:SendGrid:ApiKey is not configured.");

        // TODO (Day 3): implement with the SendGrid client (SendGridClient + SendGridMessage).
        throw new NotImplementedException("SendGrid integration is a scaffold stub.");
    }
}
