namespace Delivery.Domain.Repositories.Feed;

public interface IFeedReadOnlyRepository
{
    Task<Entities.Feed?> GetByIdAsync(long id);
    Task<List<Entities.Feed>> GetAllAsync();
}