using DocLicensing.Core.Abstractions;
using DocLicensing.Core.Dtos;
using DocLicensing.Core.Services;
using DocLicensing.Core.Validation;
using DocLicensing.Infrastructure.Data;
using DocLicensing.Infrastructure.Email;
using DocLicensing.Infrastructure.Payments;
using DocLicensing.Infrastructure.Pdf;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocLicensing.Infrastructure;

public static class DependencyInjection
{
    /// <summary>Registers data access, the provider seams, and the core services.</summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("Default")));

        services.AddScoped<IDocumentRepository, DocumentRepository>();

        // Payment provider: Fake (default) or Stripe
        if (string.Equals(config["Payments:Provider"], "Stripe", StringComparison.OrdinalIgnoreCase))
            services.AddScoped<IPaymentService, StripePaymentService>();
        else
            services.AddScoped<IPaymentService, FakePaymentService>();

        // Email provider: Log (default) or SendGrid
        if (string.Equals(config["Email:Provider"], "SendGrid", StringComparison.OrdinalIgnoreCase))
            services.AddScoped<IEmailService, SendGridEmailService>();
        else
            services.AddScoped<IEmailService, LogEmailService>();

        services.AddSingleton<IPdfService, QuestPdfWatermarkService>();

        services.AddScoped<ICatalogService, CatalogService>();
        services.AddScoped<ICheckoutService, CheckoutService>();

        services.AddScoped<IValidator<CheckoutRequest>, CheckoutRequestValidator>();
        services.AddScoped<IValidator<SearchQuery>, SearchQueryValidator>();

        return services;
    }
}
