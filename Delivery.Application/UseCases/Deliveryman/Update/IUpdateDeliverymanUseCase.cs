using Delivery.Comunication.RequestModel.Deliveryman;
using Delivery.Comunication.ResponseModel.Deliveryman;

namespace Delivery.Application.UseCases.Deliveryman.Update;

public interface IUpdateDeliverymanUseCase
{
    Task<ResponseRegisterDeliverymanJson> ExecuteAsync(int id, RequestUpdateDeliverymanJson requestRegister);
}