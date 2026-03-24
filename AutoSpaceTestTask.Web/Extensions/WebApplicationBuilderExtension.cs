using AutoSpaceTestTask.Application.Common.Constants;

namespace AutoSpaceTestTask.Web.Extensions
{
    public static class WebApplicationBuilderExtension
    {
        public static void SetUpConfigurationSources(this WebApplicationBuilder builder)
        {
            var envAppsettingFileName = $"appsettings.{builder.Environment.EnvironmentName}.json";

            builder.Configuration
                .AddJsonFile(SetupConstants.DefaultAppsettingsFilename, optional: false, reloadOnChange: true)
                .AddJsonFile(envAppsettingFileName, optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        }
    }
}
