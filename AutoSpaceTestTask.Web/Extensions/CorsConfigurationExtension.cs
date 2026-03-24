using AutoSpaceTestTask.Web.Common.Constants;

namespace AutoSpaceTestTask.Web.Extensions
{
    public static class CorsConfigurationExtension
    {
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(PoliciesConstants.CorsPolicy, policy =>
                {
                    policy.AllowAnyOrigin()     
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            return services;
        }
    }
}
