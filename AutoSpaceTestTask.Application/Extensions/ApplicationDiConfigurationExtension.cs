using AutoSpaceTestTask.Application.Services.Implemetations;
using AutoSpaceTestTask.Application.Services.Interfaces;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AutoSpaceTestTask.Application.Extensions
{
    public static class ApplicationDiConfigurationExtension
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            var assembly = typeof(ApplicationDiConfigurationExtension).Assembly;

            services
                .AddValidation(assembly)
                .AddServices()
                .AddMapping(assembly);

            return services;
        }

        private static IServiceCollection AddValidation(
            this IServiceCollection services,
            Assembly assembly)
        {
            services.AddValidatorsFromAssembly(assembly);
            return services;
        }

        private static IServiceCollection AddMapping(
        this IServiceCollection services,
        Assembly assembly)
        {
            var config = new TypeAdapterConfig();
            config.Scan(assembly);

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

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
