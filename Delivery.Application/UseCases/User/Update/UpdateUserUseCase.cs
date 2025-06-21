using AutoMapper;
using Delivery.Comunication.RequestModel.User;
using Delivery.Comunication.ResponseModel.User;
using Delivery.Domain.Repositories;
using Delivery.Domain.Repositories.User;
using Delivery.Domain.Security.Logged;
using Delivery.Exception;
using FluentValidation.Results;

namespace Delivery.Application.UseCases.User.Update;

public class UpdateUserUseCase(IUserWriteOnlyRepository repository, 
    ILoggedUser loggedUser, IMapper mapper, IUnitOfWork uow) : IUpdateUserUseCase
{
    public async Task<ResponseUserProfileJson> ExecuteAsync(RequestUpdateUserJson request)
    {
        await Validate(request);
        var user = await loggedUser.Get();
        
        mapper.Map(request, user);
        
        repository.UpdateAsync(user);

        await uow.CommitAsync();
        
        return mapper.Map<ResponseUserProfileJson>(user);

    }
    
        
    private async Task Validate(RequestUpdateUserJson requestRegister)
    {
        var validator = new UpdateUserValidator();

        var result = await validator.ValidateAsync(requestRegister);
        
        var usernameExist = await repository.ExistActiveUserWithUserNameAsync(requestRegister.UserName);
        
        if (usernameExist)
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.USERNAME_EXISTS));
        
        if (result.IsValid) return;
        
        var errorsMessage = result.Errors.Select(f => f.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorsMessage);
    }
}