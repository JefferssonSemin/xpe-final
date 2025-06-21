using Delivery.Comunication.ResponseModel.Feed;

namespace Delivery.Application.UseCases.Feed.GetById;

public interface IGetByIdFeedUseCase
{
    Task<ResponseFeedJson> ExecuteAsync(int id);
}