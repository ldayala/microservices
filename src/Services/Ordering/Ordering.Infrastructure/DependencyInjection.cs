
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;


namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
         services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(connectionString));
        //         connectionString,
        //         b => b.MigrationsAssembly(typeof(OrderingContext).Assembly.FullName)));
        // services.AddScoped<IOrderRepository, OrderRepository>();
        // services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        // services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        // services.AddScoped<IOrderItemReadRepository, OrderItemReadRepository>();
        return services;
    }
}
