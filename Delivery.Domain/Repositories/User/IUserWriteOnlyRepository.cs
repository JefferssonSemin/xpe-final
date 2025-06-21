namespace Delivery.Domain.Repositories.User;

public interface IUserWriteOnlyRepository
{
    public Task RegisterAsync(Entities.User user);
    public void UpdateAsync(Entities.User user);

    Task<bool> ExistActiveUserWithUserNameAsync(string username);
    

}