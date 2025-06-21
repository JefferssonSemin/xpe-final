using Delivery.Comunication.ResponseModel.Deliveryman;

namespace Delivery.Application.UseCases.Deliveryman.GetById;

public interface IGetByIdDeliverymanUseCase
{
    Task<ResponseDeliverymanJson> ExecuteAsync(int id);
}