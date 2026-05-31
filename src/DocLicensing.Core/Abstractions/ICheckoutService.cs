using DocLicensing.Core.Dtos;

namespace DocLicensing.Core.Abstractions;

public interface ICheckoutService
{
    Task<CheckoutResult> CheckoutAsync(CheckoutRequest request, CancellationToken ct = default);
}
