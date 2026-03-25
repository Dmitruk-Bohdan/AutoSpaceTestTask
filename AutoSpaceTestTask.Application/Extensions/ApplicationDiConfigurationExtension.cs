using AutoSpaceTestTask.Application.Services.Implemetations;
using AutoSpaceTestTask.Application.Services.Interfaces;
using AutoSpaceTestTask.Database.Extensions;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AutoSpaceTestTask.Application.Extensions
{
    public static class ApplicationDiConfigurationExtension
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var assembly = typeof(ApplicationDiConfigurationExtension).Assembly;

            services
                .AddDbConfiguration(configuration)
                .AddValidation(assembly)
                .AddServices();

            return services;
        }

        private static IServiceCollection AddValidation(
            this IServiceCollection services,
            Assembly assembly)
        {
            services.AddValidatorsFromAssembly(assembly);
            return services;
        }

        private static IServiceCollection AddServices(
        this IServiceCollection services)
        {
            services.AddScoped<IStoreManagementService, StoreManagementService>();

            return services;
        }
    }
}
