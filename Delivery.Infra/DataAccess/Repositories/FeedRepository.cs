using Delivery.Domain.Entities;
using Delivery.Domain.Repositories.Feed;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infra.DataAccess.Repositories;

public class FeedRepository(DeliveryDbContext context): IFeedWriteOnlyRepository, IFeedReadOnlyRepository
{
    public async Task AddAsync(Feed feed)
    {
        await context.Feed.AddAsync(feed);
    }

    async Task<Feed?> IFeedWriteOnlyRepository.GetByIdAsync(long id)
    {
        return await context.Feed.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    async Task<Feed?> IFeedReadOnlyRepository.GetByIdAsync(long id)
    {
        return await context.Feed.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Feed>> GetAllAsync()
    {
        return await context.Feed.AsNoTracking().ToListAsync();
    }
}