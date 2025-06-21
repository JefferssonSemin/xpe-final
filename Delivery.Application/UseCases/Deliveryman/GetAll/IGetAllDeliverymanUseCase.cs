using Delivery.Comunication.ResponseModel.Deliveryman;

namespace Delivery.Application.UseCases.Deliveryman.GetAll;

public interface IGetAllDeliverymanUseCase
{
    Task<ResponseDeliverymenJson> ExecuteAsync();
}