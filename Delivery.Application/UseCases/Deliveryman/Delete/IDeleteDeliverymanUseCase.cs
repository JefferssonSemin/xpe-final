namespace Delivery.Application.UseCases.Deliveryman.Delete;

public interface IDeleteDeliverymanUseCase
{
    Task ExecuteAsync(int id);
}