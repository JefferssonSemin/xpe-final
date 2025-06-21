namespace Delivery.Application.UseCases.Feed.Like;

public interface ILikeFeedUseCase
{
    Task ExecuteAsync(long id);
}