using DocLicensing.Core.Abstractions;
using DocLicensing.Core.Models;
using Microsoft.Extensions.Logging;

namespace DocLicensing.Infrastructure.Payments;

/// <summary>
/// Default development payment provider: always succeeds, returns a fake reference.
/// Swap for <see cref="StripePaymentService"/> by setting Payments:Provider = "Stripe".
/// </summary>
public class FakePaymentService : IPaymentService
{
    private readonly ILogger<FakePaymentService> _logger;

    public FakePaymentService(ILogger<FakePaymentService> logger) => _logger = logger;

    public Task<PaymentResult> ChargeAsync(decimal amount, string currency, string description, CancellationToken ct = default)
    {
        var reference = $"FAKE-{Guid.NewGuid():N}";
        _logger.LogInformation("Fake charge {Amount} {Currency} for '{Description}' -> {Reference}",
            amount, currency, description, reference);
        return Task.FromResult(new PaymentResult(true, reference, "Fake"));
    }
}
