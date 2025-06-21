namespace Delivery.Domain.Repositories.Deliveryman;

public interface IDeliverymanWriteOnlyRepository
{
    Task AddAsync(Entities.Deliveryman deliveryman);
    Task DeleteAsync(int id);
    void Update(Entities.Deliveryman deliveryman);
    Task<Entities.Deliveryman?> GetByIdAsync(int id, Entities.User user);
}