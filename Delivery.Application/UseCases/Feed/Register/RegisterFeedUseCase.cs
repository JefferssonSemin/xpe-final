using AutoMapper;
using Delivery.Comunication.RequestModel.Feed;
using Delivery.Comunication.ResponseModel.Feed;
using Delivery.Domain.Repositories;
using Delivery.Domain.Repositories.Feed;
using Delivery.Domain.Security.Logged;
using Delivery.Exception;

namespace Delivery.Application.UseCases.Feed.Register;

public class RegisterFeedUseCase(IMapper mapper, IUnitOfWork uow,
    ILoggedUser logged, IFeedWriteOnlyRepository writeRepository) : IRegisterFeedUseCase
{
    public async Task<ResponseFeedJson> ExecuteAsync(RequestRegisterFeedJson request)
    {
        await Validate(request);
        
        var user = await logged.Get();
        
        var feed = mapper.Map<Domain.Entities.Feed>(request);
        
        feed.AddUser(user);

        await writeRepository.AddAsync(feed);

        await uow.CommitAsync();
        
        return mapper.Map<ResponseFeedJson>(feed);
    }
    
    private static async Task Validate(RequestRegisterFeedJson requestRegister)
    {
        var validator = new RegisterFeedValidator();

        var result = await validator.ValidateAsync(requestRegister);

        if (result.IsValid) return;

        var errorsMessage = result.Errors.Select(f => f.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorsMessage);
    }
}