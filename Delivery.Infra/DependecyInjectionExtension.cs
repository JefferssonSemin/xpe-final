using Delivery.Domain.Repositories;
using Delivery.Domain.Repositories.Deliveryman;
using Delivery.Domain.Repositories.Feed;
using Delivery.Domain.Repositories.Product;
using Delivery.Domain.Repositories.User;
using Delivery.Domain.Security.Cryptography;
using Delivery.Domain.Security.Logged;
using Delivery.Domain.Security.Token;
using Delivery.Infra.DataAccess;
using Delivery.Infra.DataAccess.Repositories;
using Delivery.Infra.Security.Cryptography;
using Delivery.Infra.Security.Logged;
using Delivery.Infra.Security.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Infra;

public static class DependecyInjectionExtension
{
    public static void AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
        AddToken(services, configuration);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IPasswordEncripter, BCript>();
        
        services.AddScoped<IDeliverymanWriteOnlyRepository, DeliverymanRepository>();
        services.AddScoped<IDeliverymanReadOnlyRepository, DeliverymanRepository>();
        
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();

        services.AddScoped<ILoggedUser, LoggedUser>();
        services.AddScoped<ITokenProvider, TokenProvider>();

        services.AddScoped<IFeedWriteOnlyRepository, FeedRepository>();
        services.AddScoped<IFeedReadOnlyRepository, FeedRepository>();

        services.AddScoped<IProductWriteOnlyRepository, ProductRepository>();
        services.AddScoped<IProductReadOnlyRepository, ProductRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DeliveryDbContext>(config =>
            config.UseNpgsql(configuration.GetConnectionString("Connection")));

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(_ => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
    }
        
}