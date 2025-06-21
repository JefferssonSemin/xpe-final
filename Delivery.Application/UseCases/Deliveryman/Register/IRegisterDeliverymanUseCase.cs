using Delivery.Comunication.RequestModel.Deliveryman;
using Delivery.Comunication.ResponseModel.Deliveryman;

namespace Delivery.Application.UseCases.Deliveryman.Register;

public interface IRegisterDeliverymanUseCase
{
    Task<ResponseRegisterDeliverymanJson> ExecuteAsync(RequestRegisterDeliverymanJson requestRegister);
}