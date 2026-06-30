using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace LexiconMovieApi.Client.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        // För enkelhetens skull kör vi en hårdkodad kontroll (ersätt med databas)
        if (model.Username != "admin" || model.Password != "hemligt")
            return Unauthorized("Ogiltiga användaruppgifter.");

        // Skapa "claims" (information vi bakar in i tokenet)
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.Username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Skapa själva tokenet
        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds
        );

        return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}

// Enkel modell för inloggning
public class LoginModel
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

[ApiController]
[Route("api/[controller]")]
[Authorize] // Kräver att användaren har ett giltigt JWT
public class SecureDataController : ControllerBase
{
    [HttpGet]
    public IActionResult GetSecureData()
    {
        var userName =
        User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
        return Ok(new
        {
            Message = $"Grattis {userName}, du har nått en skyddad endpoint!" });
        }
   }
