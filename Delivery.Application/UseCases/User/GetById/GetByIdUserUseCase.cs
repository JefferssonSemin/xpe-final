using AutoMapper;
using Delivery.Comunication.ResponseModel.User;
using Delivery.Domain.Repositories.User;
using Delivery.Exception;
using Delivery.Exception.ExceptionsBase;

namespace Delivery.Application.UseCases.User.GetById;

public class GetByIdUserUseCase(IMapper mapper, IUserReadOnlyRepository repository) : IGetByIdUserUseCase
{
    public async Task<ResponseUserJson> ExecuteAsync(int id)
    {
        var user = await repository.GetByIdAsync(id);
        
        if (user == null)
            throw new NotFoundException(ResourceErrorMessages.USER_NOT_FOUND);

        return mapper.Map<ResponseUserJson>(user);

    }
}