namespace Delivery.Domain.Security.Token;

public interface ITokenProvider
{
    string TakeOnRequest();
}