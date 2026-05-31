using DocLicensing.Core.Abstractions;
using DocLicensing.Core.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocLicensing.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CheckoutController : ControllerBase
{
    private readonly ICheckoutService _checkout;
    private readonly IValidator<CheckoutRequest> _validator;

    public CheckoutController(ICheckoutService checkout, IValidator<CheckoutRequest> validator)
    {
        _checkout = checkout;
        _validator = validator;
    }

    /// <summary>Purchase a licence for a document. Requires a bearer token (see /api/auth/token).</summary>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CheckoutRequest request, CancellationToken ct = default)
    {
        var validation = await _validator.ValidateAsync(request, ct);
        if (!validation.IsValid)
            return ValidationProblem(new ValidationProblemDetails(validation.ToDictionary()));

        try
        {
            var result = await _checkout.CheckoutAsync(request, ct);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }
}
