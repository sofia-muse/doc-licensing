using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DocLicensing.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config) => _config = config;

    public record TokenRequest(string Username);

    /// <summary>
    /// DEMO ONLY: issues a signed JWT for any username, with no password check.
    /// Replace with a real identity store (ASP.NET Core Identity / Azure AD) - see README "Day 4".
    /// </summary>
    [HttpPost("token")]
    public IActionResult Token([FromBody] TokenRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username))
            return BadRequest(new { error = "Username is required." });

        var jwt = _config.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwt["Key"] ?? "dev-only-super-secret-key-change-me-please-32+chars");

        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"] ?? "doc-licensing",
            audience: jwt["Audience"] ?? "doc-licensing-clients",
            claims: new[] { new Claim(ClaimTypes.Name, request.Username) },
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));

        return Ok(new { access_token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}
