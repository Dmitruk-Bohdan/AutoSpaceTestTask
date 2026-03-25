using AutoSpaceTestTask.Application.Extensions;

namespace AutoSpaceTestTask.Web.Extensions
{
    public static class WebDiConfigurationExtension
    {
        public static IServiceCollection AddWeb(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            services
                .AddApplication(configuration)
                .AddRoutingServicesConfiguration()
                .AddHttpContextAccessor()
                .AddCorsPolicy();

            return services;
        }
    }
}
