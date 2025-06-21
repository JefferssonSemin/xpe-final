namespace Delivery.Application.UseCases.Feed.Register;

using Delivery.Comunication.RequestModel.Feed;
using Delivery.Comunication.ResponseModel.Feed;

public interface IRegisterFeedUseCase
{
    Task<ResponseFeedJson> ExecuteAsync(RequestRegisterFeedJson request);
}