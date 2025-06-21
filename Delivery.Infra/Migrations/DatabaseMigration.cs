using Delivery.Infra.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Infra.Migrations;

public static class DatabaseMigration
{
    public static async Task MigrateDatabaseAsync(IServiceProvider serviceProvider)
    {
         var dbContext = serviceProvider.GetService<DeliveryDbContext>();
         await dbContext!.Database.MigrateAsync();
    }
}