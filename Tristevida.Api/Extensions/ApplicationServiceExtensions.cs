using FluentValidation;
using Tristevida.Infrastructure.UnitOfWork;
using Tristevida.Api.Mappings;
using System.Threading.RateLimiting;
using Tristevida.Application.Abstractions;

namespace Tristevida.Api.Extensions;

public static class ApplicationServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            HashSet<string> allowed = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "https://app.tristevida.com",
                "https://admin.tristevida.com"
            };
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                       .AllowAnyMethod());
            options.AddPolicy("CorsPolicyUrl", builder =>
                builder.WithOrigins("https://localhost:4200", "https://localhost:5500")
                       .AllowAnyMethod()
                       .AllowAnyHeader());
            options.AddPolicy("Dinamica", builder =>
                builder.SetIsOriginAllowed(origin => allowed.Contains(origin))
                       .WithMethods("GET", "POST", "PUT", "DELETE")
                       .WithHeaders("Content-Type", "Authorization"));
        });

    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        services.AddAutoMapper(typeof(Program).Assembly);
        services.AddAutoMapper(typeof(CountriesProfile).Assembly);
        services.AddAutoMapper(typeof(RegionsProfile).Assembly);
        services.AddAutoMapper(typeof(CitiesProfile).Assembly);
        services.AddAutoMapper(typeof(CompaniesProfile).Assembly);
        services.AddAutoMapper(typeof(BranchesProfile).Assembly);
    }
    public static IServiceCollection AddCustomRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.OnRejected = async (context, token) =>
            {
                var ip = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "desconocida";
                context.HttpContext.Response.StatusCode = 429;
                context.HttpContext.Response.ContentType = "application/json";
                var mensaje = $"{{\"message\": \"Demasiadas peticiones desde la IP {ip}. Intenta más tarde.\"}}";
                await context.HttpContext.Response.WriteAsync(mensaje, token);
            };
            options.AddPolicy("ipLimiter", httpContext =>
            {
                var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                return RateLimitPartition.GetFixedWindowLimiter(ip, _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 10,
                    Window = TimeSpan.FromSeconds(15),
                    QueueLimit = 0,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                });
            });
        });
        return services;
    }
}
