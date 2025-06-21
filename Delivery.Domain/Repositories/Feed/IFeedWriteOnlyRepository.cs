namespace Delivery.Domain.Repositories.Feed;

public interface IFeedWriteOnlyRepository
{
    Task AddAsync(Entities.Feed feed);
    Task<Entities.Feed?> GetByIdAsync(long id);
}