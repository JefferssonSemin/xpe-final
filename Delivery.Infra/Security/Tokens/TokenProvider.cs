using Delivery.Domain.Security.Token;
using Microsoft.AspNetCore.Http;

namespace Delivery.Infra.Security.Tokens;

public class TokenProvider(IHttpContextAccessor httpContextAccessor): ITokenProvider
{
    public string TakeOnRequest()
    {
        var authorization = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

        return authorization["Bearer ".Length..].Trim();
    }
}