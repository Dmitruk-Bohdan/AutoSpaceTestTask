using AutoSpaceTestTask.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace AutoSpaceTestTask.Database.Extensions;

public static class DbMigrationExtension
{
    public static WebApplication ApplyDbMigrations(this WebApplication app)
    {
#if !DEBUG        
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
        }
#endif
        return app;
    }
}