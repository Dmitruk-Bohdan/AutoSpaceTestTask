using AutoSpaceTestTask.Database.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AutoSpaceTestTask.Database.Extensions;

public static class DbMigrationExtension
{
    public static WebApplication ApplyDbMigrations(this WebApplication app)
    {
#if !DEBUG
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var retryCount = 5;
        var delay = TimeSpan.FromSeconds(5);
        for (int i = 0; i < retryCount; i++)
        {
            try
            {
                db.Database.Migrate();
                break;
            }
            catch (SqlException ex) when (ex.Number == 1801)
            {
                if (db.Database.CanConnect())
                {
                    continue;
                }
                throw;
            }
            catch (SqlException) when (i < retryCount - 1)
            {
                Thread.Sleep(delay);
            }
        }
#endif
        return app;
    }
}