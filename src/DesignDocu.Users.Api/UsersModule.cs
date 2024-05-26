using DesignDocu.Common.Module;
using DesignDocu.Users.Api.Endpoints;
using DesignDocu.Users.Application;
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
        services.AddUsersApplication();
        services.AddUsersInfrastructure(configuration);
    }

    public void ConfigureEndpoints(WebApplication application)
    {
        application.MapAuthEndpoints();
        application.MapGroup("/users").MapUserEndpoints();
    }
}