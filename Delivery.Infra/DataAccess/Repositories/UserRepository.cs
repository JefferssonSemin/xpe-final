using Delivery.Domain.Entities;
using Delivery.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Infra.DataAccess.Repositories;

internal class UserRepository(DeliveryDbContext context) : IUserWriteOnlyRepository, IUserReadOnlyRepository
{
    public async Task RegisterAsync(User user)
    {
        await context.User.AddAsync(user);
    }

    public void UpdateAsync(User user)
    {
        context.User.Update(user);
    }

    public async Task<bool> ExistActiveUserWithUserNameAsync(string username)
    {
        return await context.User
            .AnyAsync(u => u.UserName == username); 
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await context.User.ToListAsync();
    }
    
    public async Task<User?> GetByIdAsync(int id)
    {
        return await context.User
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await context.User.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == username);
    }
}