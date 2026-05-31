using DocLicensing.Core.Abstractions;
using DocLicensing.Core.Dtos;
using DocLicensing.Core.Models;
using DocLicensing.Core.Services;
using Xunit;

namespace DocLicensing.Tests;

public class CheckoutServiceTests
{
    [Fact]
    public async Task CheckoutAsync_charges_persists_emails_and_returns_download_url()
    {
        var repo = new InMemoryDocumentRepository();
        var payment = new StubPaymentService();
        var email = new StubEmailService();
        var sut = new CheckoutService(repo, payment, email);

        var result = await sut.CheckoutAsync(new CheckoutRequest
        {
            DocumentId = 1,
            BuyerName = "Sofia",
            BuyerEmail = "sofia@example.com"
        });

        Assert.Equal(1, payment.ChargeCalls);
        Assert.Equal(1, email.SendCalls);
        Assert.Equal("STUB-REF", result.PaymentReference);
        Assert.Equal(129.00m, result.AmountPaid);
        Assert.Contains("/api/documents/1/download", result.DownloadUrl);
        Assert.Single(repo.Purchases);
    }

    [Fact]
    public async Task CheckoutAsync_unknown_document_throws()
    {
        var sut = new CheckoutService(new InMemoryDocumentRepository(), new StubPaymentService(), new StubEmailService());

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            sut.CheckoutAsync(new CheckoutRequest { DocumentId = 999, BuyerName = "x", BuyerEmail = "x@y.z" }));
    }

    private sealed class InMemoryDocumentRepository : IDocumentRepository
    {
        public List<Purchase> Purchases { get; } = new();

        private readonly List<Document> _docs = new()
        {
            new Document { Id = 1, Code = "ISO-9001", TitleEn = "Quality", Price = 129.00m, Currency = "ILS" }
        };

        public Task<IReadOnlyList<Document>> GetAllAsync(CancellationToken ct = default) =>
            Task.FromResult<IReadOnlyList<Document>>(_docs);

        public Task<Document?> GetByIdAsync(int id, CancellationToken ct = default) =>
            Task.FromResult(_docs.FirstOrDefault(d => d.Id == id));

        public Task<Purchase> AddPurchaseAsync(Purchase purchase, CancellationToken ct = default)
        {
            purchase.Id = Purchases.Count + 1;
            Purchases.Add(purchase);
            return Task.FromResult(purchase);
        }

        public Task<Purchase?> GetPurchaseAsync(int id, CancellationToken ct = default) =>
            Task.FromResult(Purchases.FirstOrDefault(p => p.Id == id));
    }

    private sealed class StubPaymentService : IPaymentService
    {
        public int ChargeCalls { get; private set; }

        public Task<PaymentResult> ChargeAsync(decimal amount, string currency, string description, CancellationToken ct = default)
        {
            ChargeCalls++;
            return Task.FromResult(new PaymentResult(true, "STUB-REF", "Stub"));
        }
    }

    private sealed class StubEmailService : IEmailService
    {
        public int SendCalls { get; private set; }

        public Task SendReceiptAsync(string toEmail, Document document, PaymentResult payment, CancellationToken ct = default)
        {
            SendCalls++;
            return Task.CompletedTask;
        }
    }
}
