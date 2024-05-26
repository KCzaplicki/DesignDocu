using System.Text;
using DesignDocu.Infrastructure.Auth.Authorization.Core.Authorization;
using DesignDocu.Infrastructure.Auth.Authorization.Core.DataAccess;
using DesignDocu.Infrastructure.Auth.Authorization.Core.Jwt;
using DesignDocu.Infrastructure.Authorization.Abstractions.Authorization;
using DesignDocu.Infrastructure.Authorization.Abstractions.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DesignDocu.Infrastructure.Auth.Authorization.Core;

public static class ServiceExtensions
{
    public static void AddIdentityAuthorization(this IServiceCollection services, IConfigurationSection configuration)
    {
        var authorizationConfiguration = configuration.Get<AuthorizationConfiguration>()
                                 ?? throw new ArgumentNullException(nameof(AuthorizationConfiguration), 
                                     "Authorization configuration not found in appsettings.json");
        services.AddSingleton(authorizationConfiguration);

        services.AddDbContext<AuthorizationContext>(options =>
        {
            options.UseSqlServer(authorizationConfiguration.ConnectionString);
        });
        
        services.AddIdentityApiEndpoints<ApplicationUser>()
            .AddEntityFrameworkStores<AuthorizationContext>()
            .AddDefaultTokenProviders();

        var key = Encoding.ASCII.GetBytes(authorizationConfiguration.JwtSettings.SigningKey);
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = authorizationConfiguration.JwtSettings.Issuer,
                    ValidAudience = authorizationConfiguration.JwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        
        services.AddAuthorization();
        
        services.AddSingleton<ITokenProvider, JwtTokenProvider>();
        services.AddScoped<IAuthorizationService, AuthorizationService>();
    }

    public static IApplicationBuilder UseIdentityAuthorization(this IApplicationBuilder builder)
    {
        builder.UseAuthentication();
        builder.UseAuthorization();
        
        return builder;
    }
}