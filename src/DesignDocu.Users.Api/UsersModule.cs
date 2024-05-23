using DesignDocu.Common.Module;
using DesignDocu.Users.Api.Healthcheck;
using DesignDocu.Users.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DesignDocu.Users.Api;

public class UsersModule : IModule
{
    public string ModuleName => "Users";

    public void ConfigureServices(IServiceCollection services, IConfigurationSection configuration)
    {
        services.AddUsersInfrastructure(configuration);
    }

    public void Configure(WebApplication application)
    {
        application.MapGroup("/users")
            .MapHealthcheck();
    }
}