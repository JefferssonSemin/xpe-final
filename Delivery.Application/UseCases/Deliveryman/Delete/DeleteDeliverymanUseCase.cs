using Delivery.Domain.Repositories;
using Delivery.Domain.Repositories.Deliveryman;
using Delivery.Domain.Security.Logged;
using Delivery.Exception;
using Delivery.Exception.ExceptionsBase;

namespace Delivery.Application.UseCases.Deliveryman.Delete;

public class DeleteDeliverymanUseCase(
    IDeliverymanWriteOnlyRepository repository,
    IUnitOfWork uow, 
    ILoggedUser loggedUser)
    : IDeleteDeliverymanUseCase
{
    public async Task ExecuteAsync(int id)
    {
        var user = await loggedUser.Get();

        var deliveryman = await repository.GetByIdAsync(id, user);
        
        if (deliveryman is null) throw new NotFoundException(ResourceErrorMessages.DELIVERYMAN_NOT_FOUND);

        await repository.DeleteAsync(id);
        
        await uow.CommitAsync();
    }
}