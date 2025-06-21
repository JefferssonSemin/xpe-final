using Delivery.Comunication.ResponseModel.User;

namespace Delivery.Application.UseCases.User.GetById;

public interface IGetByIdUserUseCase
{
    Task<ResponseUserJson> ExecuteAsync(int id);
}