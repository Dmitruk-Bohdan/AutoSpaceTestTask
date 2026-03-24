using AutoSpaceTestTask.Application.Extensions;

namespace AutoSpaceTestTask.Web.Extensions
{
    public static class WebDiConfigurationExtension
    {
        public static IServiceCollection AddWeb(
           this IServiceCollection services)
        {
            services
                .AddApplication()
                .AddRoutingServicesConfiguration()
                .AddHttpContextAccessor()
                .AddCorsPolicy();

            return services;
        }
    }
}
