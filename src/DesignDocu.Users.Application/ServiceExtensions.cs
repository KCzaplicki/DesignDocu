using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DesignDocu.Users.Application;

public static class ServiceExtensions
{
    public static IServiceCollection AddUsersApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        
        return services;
    }
}