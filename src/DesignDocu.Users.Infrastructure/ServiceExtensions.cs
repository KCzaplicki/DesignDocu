using DesignDocu.Users.Infrastructure.DataAccess;
using DesignDocu.Users.Infrastructure.DataAccess.Repositories;
using DesignDocu.Users.Infrastructure.Identity;
using DessignDocu.Users.Domain.Users.Repositories;
using DessignDocu.Users.Domain.Users.Services;
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
        
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}