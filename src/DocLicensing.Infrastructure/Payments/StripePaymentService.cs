using DocLicensing.Core.Abstractions;
using DocLicensing.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DocLicensing.Infrastructure.Payments;

/// <summary>
/// Skeleton Stripe integration. Wire Stripe.net (PaymentIntents / Checkout Sessions) here.
/// Reads Payments:Stripe:SecretKey from configuration / user-secrets - never commit a key.
/// See README "Day 3" for the implementation steps.
/// </summary>
public class StripePaymentService : IPaymentService
{
    private readonly string? _secretKey;
    private readonly ILogger<StripePaymentService> _logger;

    public StripePaymentService(IConfiguration config, ILogger<StripePaymentService> logger)
    {
        _secretKey = config["Payments:Stripe:SecretKey"];
        _logger = logger;
    }

    public Task<PaymentResult> ChargeAsync(decimal amount, string currency, string description, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(_secretKey))
            throw new InvalidOperationException(
                "Payments:Stripe:SecretKey is not configured. Add it via user-secrets or environment variables.");

        _logger.LogWarning("StripePaymentService is a scaffold stub and is not implemented yet.");

        // TODO (Day 3): implement with Stripe.net in test mode, e.g.
        //   StripeConfiguration.ApiKey = _secretKey;
        //   var intent = await new PaymentIntentService().CreateAsync(new PaymentIntentCreateOptions
        //   {
        //       Amount = (long)(amount * 100),
        //       Currency = currency.ToLowerInvariant(),
        //       Description = description
        //   }, cancellationToken: ct);
        //   return new PaymentResult(intent.Status == "succeeded", intent.Id, "Stripe");
        throw new NotImplementedException("Stripe integration is a scaffold stub - implement with Stripe.net.");
    }
}
