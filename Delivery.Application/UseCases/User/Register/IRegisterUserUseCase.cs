using Delivery.Comunication.RequestModel.User;
using Delivery.Comunication.ResponseModel.User;

namespace Delivery.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    public Task<ResponseUserJson> ExecuteAsync(RequestRegisterUserJson request);
}