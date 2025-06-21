using AutoMapper;
using Delivery.Comunication.ResponseModel.User;
using Delivery.Domain.Repositories.User;

namespace Delivery.Application.UseCases.User.GetAll;

public class GetAllUserUseCase(IMapper mapper, IUserReadOnlyRepository repository) : IGetAllUserUseCase
{
    public async Task<List<ResponseUserProfileJson>> ExecuteAsync()
    {
        var users = await repository.GetAllAsync();
        
        var result = mapper.Map<List<ResponseUserProfileJson>>(users);

        return result;
    }
}