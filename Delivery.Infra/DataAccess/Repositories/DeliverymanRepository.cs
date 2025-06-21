using Delivery.Domain.Entities;
using Delivery.Domain.Repositories.Deliveryman;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infra.DataAccess.Repositories;

internal class DeliverymanRepository(DeliveryDbContext context)
    : IDeliverymanWriteOnlyRepository, IDeliverymanReadOnlyRepository
{
    public async Task<List<Deliveryman>> GetAllAsync()
    {
        return await context.Deliveryman.AsNoTracking().ToListAsync();
    }

    async Task<Deliveryman?> IDeliverymanReadOnlyRepository.GetByIdAsync(int id)
    {
        return await context.Deliveryman.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<List<Deliveryman>> FiltersByMonthBirth(User user, DateOnly month)
    {
        return await context.Deliveryman.AsNoTracking()
            .AsNoTracking()
            .Where(d => d.UserId == user.Id && d.DateOfBirth.HasValue && d.DateOfBirth.Value.Month == month.Month)
            .OrderBy(delivery => delivery.DateOfBirth)
            .ToListAsync();
    }

    async Task<Deliveryman?> IDeliverymanWriteOnlyRepository.GetByIdAsync(int id, User user)
    {
        return await context.Deliveryman.FirstOrDefaultAsync(d => d.Id == id && d.UserId == user.Id);
    }

    public async Task AddAsync(Deliveryman deliveryman)
    {
        await context.Deliveryman.AddAsync(deliveryman);
    }

    public async Task DeleteAsync(int id)
    {
        var deliveryman = await context.Deliveryman.FirstAsync(d => d.Id == id);
        
        context.Deliveryman.Remove(deliveryman);
    }

    public void Update(Deliveryman deliveryman)
    {
        context.Deliveryman.Update(deliveryman);
    }
}