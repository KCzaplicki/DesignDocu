using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DesignDocu.Common.Api.Configuration;

public static class ConfigurationExtensions
{
    public static IConfigurationManager AddJsonFileConfiguration(this IConfigurationManager configuration, IWebHostEnvironment environment)
    {
        configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
        
        return configuration;
    }
}