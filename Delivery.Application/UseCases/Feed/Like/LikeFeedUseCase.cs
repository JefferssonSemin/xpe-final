using Delivery.Domain.Repositories;
using Delivery.Domain.Repositories.Feed;
using Delivery.Exception;
using Delivery.Exception.ExceptionsBase;

namespace Delivery.Application.UseCases.Feed.Like;

public class LikeFeedUseCase(IFeedWriteOnlyRepository repository, IUnitOfWork uow) : ILikeFeedUseCase
{
    public async Task ExecuteAsync(long id)
    {
        var feed = await repository.GetByIdAsync(id);

        if (feed == null) throw new NotFoundException(ResourceErrorMessages.FEED_NOT_FOUND);
        
        feed.UpdateLike();
        
        await uow.CommitAsync();
    }
}