using System.Text;
using Delivery.Application;
using Delivery.Extensions;
using Delivery.Filters;
using Delivery.Infra;
using Delivery.Infra.DataAccess;
using Delivery.Infra.Migrations;
using Delivery.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddInfra(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplication();
builder.Services.AddSwaggerGen();

builder.Host.SerilogConfiguration();

builder.Services.AddHealthChecks().AddDbContextCheck<DeliveryDbContext>();

builder.WebHost.UseSentry(options =>
{
    options.Dsn = "https://4ba5545cfda8c237b6244c9093daf6e8@o4509367684825088.ingest.us.sentry.io/4509367721852928";
    options.Debug = true;
    options.TracesSampleRate = 1.0;
    options.SendDefaultPii = true;
    options.Environment = builder.Configuration["ASPNETCORE_ENVIRONMENT"] ?? "Production";
});

var signingKey = builder.Configuration.GetValue<string>("Settings:Jwt:SigningKey");

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = new TimeSpan(0),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey!))
    };
});

var app = builder.Build();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    AllowCachingResponses = false,
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
});

app.UseSwagger();
app.MapScalarApiReference(opt =>
{
    opt.Title = "Delivery API";
    opt.Theme = ScalarTheme.DeepSpace;
    opt.OpenApiRoutePattern = "swagger/v1/swagger.json"; 
});

app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseSentryTracing();

app.UseAuthentication();

app.MapControllers();

await MigrateDatabase();

app.Run();

return;

async Task MigrateDatabase()
{
    await using var scope = app.Services.CreateAsyncScope();
    await DatabaseMigration.MigrateDatabaseAsync(scope.ServiceProvider);
}