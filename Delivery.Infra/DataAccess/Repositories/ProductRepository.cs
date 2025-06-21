using Delivery.Domain.Entities;
using Delivery.Domain.Repositories.Product;

namespace Delivery.Infra.DataAccess.Repositories;

public class ProductRepository(DeliveryDbContext context) : IProductWriteOnlyRepository, IProductReadOnlyRepository
{
    public async Task SaveAsync(Product product)
    {
        await context.AddAsync(product);
    }
}