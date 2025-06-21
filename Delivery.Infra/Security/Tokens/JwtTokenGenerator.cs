using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Delivery.Domain.Entities;
using Delivery.Domain.Security.Token;
using Microsoft.IdentityModel.Tokens;

namespace Delivery.Infra.Security.Tokens;

public class JwtTokenGenerator(uint expirationTimeMinutes, string signinKey) : IAccessTokenGenerator
{
    public string Generate(User user)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, user.UserName),
            new (ClaimTypes.Sid, user.Guid.ToString()),
            new (ClaimTypes.Role, user.Role)
        };
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(securityToken);
    }

    private SymmetricSecurityKey SecurityKey()
    {
        var key = Encoding.UTF8.GetBytes(signinKey);
        return new SymmetricSecurityKey(key);
    }
}