namespace Delivery.Domain.Repositories.Deliveryman;

public interface IDeliverymanReadOnlyRepository
{
    Task<List<Entities.Deliveryman>> GetAllAsync();
    Task<Entities.Deliveryman?> GetByIdAsync(int id);
    Task<List<Entities.Deliveryman>> FiltersByMonthBirth(Entities.User user, DateOnly month);
}