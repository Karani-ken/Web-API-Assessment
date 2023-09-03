using Microsoft.EntityFrameworkCore;
using Web_API_Assessment.Data;

namespace Web_API_Assessment.Extensions
{
    public static class AddMigrations
    {
        public static IApplicationBuilder ApplyMigration(this IApplicationBuilder app)
        {

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }

            return app;
        }
    }
}
