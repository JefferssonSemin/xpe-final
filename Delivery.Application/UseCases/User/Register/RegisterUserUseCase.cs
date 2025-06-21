using AutoMapper;
using Delivery.Comunication.RequestModel.User;
using Delivery.Comunication.ResponseModel.User;
using Delivery.Domain.Repositories;
using Delivery.Domain.Repositories.User;
using Delivery.Domain.Security.Cryptography;
using Delivery.Domain.Security.Token;
using Delivery.Exception;
using FluentValidation.Results;

namespace Delivery.Application.UseCases.User.Register;

public class RegisterUserUseCase(
    IMapper mapper, IUserWriteOnlyRepository repository, IUnitOfWork uow,
    IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator ) : IRegisterUserUseCase
{
    public async Task<ResponseUserJson> ExecuteAsync(RequestRegisterUserJson request)
    {
        await Validate(request);
        
        var user = mapper.Map<Domain.Entities.User>(request);
        user.AddPassword(passwordEncripter.Encrypt(request.Password));

        await repository.RegisterAsync(user);

        await uow.CommitAsync();

        return new ResponseUserJson
        {
            UserName = user.UserName,
            Token = accessTokenGenerator.Generate(user)
        };
    }
    
    private async Task Validate(RequestRegisterUserJson requestRegister)
    {
        var validator = new RegisterUserValidator();

        var result = await validator.ValidateAsync(requestRegister);
        
        var usernameExist = await repository.ExistActiveUserWithUserNameAsync(requestRegister.UserName);
        
        if (usernameExist)
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.USERNAME_EXISTS));
        
        if (result.IsValid) return;
        
        var errorsMessage = result.Errors.Select(f => f.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorsMessage);
    }
}