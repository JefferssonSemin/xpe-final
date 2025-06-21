using Delivery.Comunication.RequestModel.User;
using Delivery.Comunication.ResponseModel.User;

namespace Delivery.Application.UseCases.User.Update;

public interface IUpdateUserUseCase
{
    Task<ResponseUserProfileJson> ExecuteAsync(RequestUpdateUserJson request);
}