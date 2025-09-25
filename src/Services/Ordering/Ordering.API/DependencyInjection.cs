namespace Ordering.API;

public static class DependencyInjection
{
   public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // services.AddControllers();
        // services.AddEndpointsApiExplorer();
        // services.AddSwaggerGen();
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        // if (app.Environment.IsDevelopment())
        // {
        //     app.UseSwagger();
        //     app.UseSwaggerUI();
        // }
        // app.UseHttpsRedirection();
        // app.UseAuthorization();
        // app.MapControllers();
        return app;
    }
}
