using DocLicensing.Core.Abstractions;
using DocLicensing.Core.Dtos;
using DocLicensing.Core.Models;

namespace DocLicensing.Core.Services;

/// <summary>
/// Orchestrates a purchase: charge -> persist -> email receipt -> return a download link.
/// Mirrors the SII purchase flow in a generic, publishable form.
/// </summary>
public class CheckoutService : ICheckoutService
{
    private readonly IDocumentRepository _repo;
    private readonly IPaymentService _payment;
    private readonly IEmailService _email;

    public CheckoutService(IDocumentRepository repo, IPaymentService payment, IEmailService email)
    {
        _repo = repo;
        _payment = payment;
        _email = email;
    }

    public async Task<CheckoutResult> CheckoutAsync(CheckoutRequest request, CancellationToken ct = default)
    {
        var document = await _repo.GetByIdAsync(request.DocumentId, ct)
            ?? throw new KeyNotFoundException($"Document {request.DocumentId} not found.");

        var payment = await _payment.ChargeAsync(
            document.Price,
            document.Currency,
            $"License: {document.Code} - {document.TitleEn}",
            ct);

        if (!payment.Succeeded)
            throw new InvalidOperationException("Payment failed.");

        var purchase = await _repo.AddPurchaseAsync(new Purchase
        {
            DocumentId = document.Id,
            BuyerName = request.BuyerName,
            BuyerEmail = request.BuyerEmail,
            AmountPaid = document.Price,
            Currency = document.Currency,
            PaymentReference = payment.Reference
        }, ct);

        await _email.SendReceiptAsync(request.BuyerEmail, document, payment, ct);

        return new CheckoutResult(
            purchase.Id,
            payment.Reference,
            purchase.AmountPaid,
            purchase.Currency,
            $"/api/documents/{document.Id}/download?purchase={purchase.Id}");
    }
}
