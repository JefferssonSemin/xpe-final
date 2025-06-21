namespace Delivery.Domain.Security.Logged;

public interface ILoggedUser
{
    Task<Entities.User> Get();
}