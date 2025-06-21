using Delivery.Comunication.ResponseModel.User;
using Delivery.Domain.Repositories.User;
using Delivery.Domain.Security.Cryptography;
using Delivery.Domain.Security.Token;
using Delivery.Exception;
using Microsoft.Extensions.Logging;

namespace Delivery.Application.UseCases.User.Login;

public class DoLoginUseCase (IUserReadOnlyRepository repository, IPasswordEncripter passwordEncripter,
    IAccessTokenGenerator tokenGenerator, ILogger<DoLoginUseCase> log) : IDoLoginUseCase
{
    public async Task<ResponseUserJson> ExecuteAsync(string username, string password)
    {
        var user = await repository.GetByUsernameAsync(username);
        
        log.LogInformation("get user {userName} ", user!.UserName);

        if (user is null) throw new InvalidLoginException();
        
        log.LogInformation("user is not null");
        
        var passwordIsValid = passwordEncripter.Verify(password, user.Password);

        if (!passwordIsValid)  throw new InvalidLoginException();
        
        log.LogInformation("password is valid");
        
        return new ResponseUserJson
        {
            UserName = user.UserName,
            Token = tokenGenerator.Generate(user)
        };
    }
}