using Delivery.Comunication.ResponseModel.User;

namespace Delivery.Application.UseCases.User.GetAll;

public interface IGetAllUserUseCase
{
    public Task<List<ResponseUserProfileJson>> ExecuteAsync();
}