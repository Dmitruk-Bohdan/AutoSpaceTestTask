namespace AutoSpaceTestTask.Web.Extensions
{
    public static class RoutingConfigurationExtension
    {
        public static IServiceCollection AddRoutingServicesConfiguration(this IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddRouting();

            return services;
        }

        public static void AddWebApplicationRoutingConfigurations(this WebApplication app)
        {
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

        }
    }
}
