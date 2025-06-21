namespace Delivery.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    public Task<List<Entities.User>> GetAllAsync();
    Task<Entities.User?> GetByIdAsync(int id);
    Task<Entities.User?> GetByUsernameAsync(string username );

}