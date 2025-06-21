using Delivery.Domain.Repositories;

namespace Delivery.Infra.DataAccess.Repositories;

public class UnitOfWork(DeliveryDbContext context) : IUnitOfWork
{
    public async Task CommitAsync()
    {
        await context.SaveChangesAsync();
    }
}