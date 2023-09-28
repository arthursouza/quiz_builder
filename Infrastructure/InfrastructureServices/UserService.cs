using ApplicationCore.Configurations;
using ApplicationCore.Exceptions;
using ApplicationCore.Models.User;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.InfrastructureServices;
public class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;

    public UserService(UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task CreateAsync(AuthModel model)
    {
        var user = new IdentityUser()
        {
            Email = model.Email,
            UserName = model.Email
        };

        var identityResult = await _userManager.CreateAsync(user, model.Password);
        if (!identityResult.Succeeded)
        {
            throw new UserValidationException(
                string.Join(
                    Environment.NewLine,
                    identityResult.Errors.Select(e => e.Description)));
        }
    }

    public async Task<string> Authenticate(AuthModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Email);

        if (!await _userManager.CheckPasswordAsync(user, model.Password))
        {
            return null;
        }

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        };

        return GenerateToken(authClaims);
    }

    private string GenerateToken(IEnumerable<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[JWTConfiguration.Secret]));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(Convert.ToInt32(_configuration[JWTConfiguration.TokenExpiryTimeInHours])),
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration[JWTConfiguration.ValidIssuer],
            Audience = _configuration[JWTConfiguration.ValidAudience],
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
