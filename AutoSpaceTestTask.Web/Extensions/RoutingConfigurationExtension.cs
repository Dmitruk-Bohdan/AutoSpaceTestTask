using AutoSpaceTestTask.Database.Common.Converters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AutoSpaceTestTask.Web.Extensions
{
    public static class RoutingConfigurationExtension
    {
        public static IServiceCollection AddRoutingServicesConfiguration(this IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });
            services.AddHttpContextAccessor();
            services.AddRouting();

            return services;
        }

        public static void AddWebApplicationRoutingConfigurations(this WebApplication app)
        {
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Store}/{action=Index}");

        }
    }
}
