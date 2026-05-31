using DocLicensing.Core.Abstractions;
using DocLicensing.Core.Models;
using Microsoft.Extensions.Logging;

namespace DocLicensing.Infrastructure.Email;

/// <summary>Default development email provider: logs the receipt instead of sending.</summary>
public class LogEmailService : IEmailService
{
    private readonly ILogger<LogEmailService> _logger;

    public LogEmailService(ILogger<LogEmailService> logger) => _logger = logger;

    public Task SendReceiptAsync(string toEmail, Document document, PaymentResult payment, CancellationToken ct = default)
    {
        _logger.LogInformation("Receipt -> {To}: {Code} '{Title}' paid via {Provider}, ref {Reference}",
            toEmail, document.Code, document.TitleEn, payment.Provider, payment.Reference);
        return Task.CompletedTask;
    }
}
