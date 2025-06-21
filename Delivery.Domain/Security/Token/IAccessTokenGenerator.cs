namespace Delivery.Domain.Security.Token;

using Entities;

public interface IAccessTokenGenerator
{
    string Generate(User user);
}