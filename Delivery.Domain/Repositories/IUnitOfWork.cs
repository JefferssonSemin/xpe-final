namespace Delivery.Domain.Repositories;

public interface IUnitOfWork
{
    Task CommitAsync();
}