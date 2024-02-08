using System.Security.Claims;
using System.Text;
using api.Entities;
using api.Interfaces;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace api.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
  private readonly SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(configuration["TokenKey"] ?? throw new InvalidOperationException("Missing token security key.")));
  public string CreateToken(User user)
  {
    var claims = new List<Claim>
    {
      new(JwtRegisteredClaimNames.NameId, user.Username)
    };
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.Now.AddDays(1),
      SigningCredentials = creds,
    };
    var tokenHandler = new JsonWebTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return token;
  }
}
