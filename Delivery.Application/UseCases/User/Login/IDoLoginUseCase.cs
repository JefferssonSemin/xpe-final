using Delivery.Comunication.ResponseModel.User;

namespace Delivery.Application.UseCases.User.Login;

public interface IDoLoginUseCase
{
    Task<ResponseUserJson>  ExecuteAsync(string email, string password);
}