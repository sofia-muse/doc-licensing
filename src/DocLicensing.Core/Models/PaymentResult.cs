namespace DocLicensing.Core.Models;

/// <summary>Outcome of a payment attempt, provider-agnostic.</summary>
public record PaymentResult(bool Succeeded, string Reference, string Provider);
