using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Delivery.Domain.Security.Logged;
using Delivery.Domain.Security.Token;
using Delivery.Infra.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infra.Security.Logged;

public class LoggedUser(DeliveryDbContext context, ITokenProvider tokenProvider) : ILoggedUser
{
    public async Task<Domain.Entities.User> Get()
    {
        var token = tokenProvider.TakeOnRequest();
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        return await context.User.AsNoTracking().FirstAsync(user => user.Guid == Guid.Parse(identifier));
    }
}