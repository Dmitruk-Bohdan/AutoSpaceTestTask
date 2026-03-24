using AutoSpaceTestTask.Database.Common.Constants;
using AutoSpaceTestTask.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutoSpaceTestTask.Database.Extensions;

public static class DatabaseDiConfigurationExtension
{
    public static IServiceCollection AddDbConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(
            DbConfigurationConstants.DefaultDbConnectionKey);

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(
                connectionString,
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                });
        });

        return services;
    }
}