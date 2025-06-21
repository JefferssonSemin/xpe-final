namespace Delivery.Domain.Repositories.Product;

public interface IProductWriteOnlyRepository
{
    Task SaveAsync(Entities.Product product);
}