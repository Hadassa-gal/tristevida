using Tristevida.Api.Extensions;
using Tristevida.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();

// Extensiones personalizadas
builder.Services.ConfigureCors();
builder.Services.AddCustomRateLimiter();
builder.Services.AddApplicationServices();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("Postgres")!;
    options.UseNpgsql(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

var app = builder.Build();

app.UseCors("CorsPolicy");
app.UseCors("CorsPolicyUrl");
app.UseCors("Dinamica");

app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthorization();

app.MapControllers();

app.Run();
