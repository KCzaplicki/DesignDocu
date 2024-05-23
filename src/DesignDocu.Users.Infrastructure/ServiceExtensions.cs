using DesignDocu.Users.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DesignDocu.Users.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddUsersInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var usersConfiguration = configuration.Get<UsersConfiguration>()
                                 ?? throw new ArgumentNullException(nameof(UsersConfiguration), 
                                     "Users module configuration not found in appsettings.json");
        services.AddSingleton(usersConfiguration);
        
        services.AddDbContext<UsersContext>(options =>
        {
            options.UseSqlServer(usersConfiguration.ConnectionString);
        });
        
        return services;
    }
}