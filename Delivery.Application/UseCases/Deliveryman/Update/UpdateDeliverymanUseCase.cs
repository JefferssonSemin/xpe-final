using AutoMapper;
using Delivery.Comunication.RequestModel.Deliveryman;
using Delivery.Comunication.ResponseModel.Deliveryman;
using Delivery.Domain.Repositories;
using Delivery.Domain.Repositories.Deliveryman;
using Delivery.Domain.Security.Logged;
using Delivery.Exception;
using Delivery.Exception.ExceptionsBase;

namespace Delivery.Application.UseCases.Deliveryman.Update;

public class UpdateDeliverymanUseCase(
    IDeliverymanWriteOnlyRepository repository, 
    IMapper mapper, 
    IUnitOfWork uow,
    ILoggedUser loggedUser)
    : IUpdateDeliverymanUseCase
{
    public async Task<ResponseRegisterDeliverymanJson> ExecuteAsync(int id, RequestUpdateDeliverymanJson requestUpdate)
    {
        Validate(requestUpdate);

        var user = await loggedUser.Get();

        var deliveryman = await repository.GetByIdAsync(id, user);

        if (deliveryman is null)
            throw new NotFoundException(ResourceErrorMessages.DELIVERYMAN_NOT_FOUND);

        mapper.Map(requestUpdate, deliveryman);

        repository.Update(deliveryman);

        await uow.CommitAsync();

        return mapper.Map<ResponseRegisterDeliverymanJson>(deliveryman);
    }

    private static void Validate(RequestUpdateDeliverymanJson requestUpdate)
    {
        var validator = new UpdateDeliveryValidator();

        var result = validator.Validate(requestUpdate);

        if (result.IsValid) return;

        var errorsMessage = result.Errors.Select(f => f.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorsMessage);
    }
}