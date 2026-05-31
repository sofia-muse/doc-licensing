using DocLicensing.Core.Models;

namespace DocLicensing.Core.Abstractions;

/// <summary>Payment provider seam. Default impl is a fake; Stripe plugs in behind this.</summary>
public interface IPaymentService
{
    Task<PaymentResult> ChargeAsync(decimal amount, string currency, string description, CancellationToken ct = default);
}
