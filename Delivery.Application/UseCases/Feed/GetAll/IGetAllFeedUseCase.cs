using Delivery.Comunication.ResponseModel.Feed;

namespace Delivery.Application.UseCases.Feed.GetAll;

public interface IGetAllFeedUseCase
{
    Task<List<ResponseFeedJson>> ExecuteAsync();
}