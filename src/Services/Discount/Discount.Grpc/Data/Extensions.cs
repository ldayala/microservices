using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Discount.Grpc.Data
{
    //creamos esta extension para ejecutar la migracion cuando se inicie el contenedor
    public static class Extensions
    {
        public static async Task<IApplicationBuilder> UseMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext= scope.ServiceProvider.GetRequiredService<DiscountContext>();
           await dbContext.Database.MigrateAsync();
            return app;
        }
    }
}
