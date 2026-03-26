using AutoSpaceTestTask.Database.Extensions;
using AutoSpaceTestTask.Web.Common.Constants;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace AutoSpaceTestTask.Web.Extensions
{
    public static class WebApplicationExtension
    {
        public static void ConfigureMiddleware(this WebApplication app)
        {
            app.UseGlobalExceptionHandling();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(PoliciesConstants.CorsPolicy);
            app.ApplyDbMigrations();
            app.SeedDb();
            app.MapControllers();
            app.AddWebApplicationRoutingConfigurations();
        }
    }
}
