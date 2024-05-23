using System.Text;
using DesignDocu.Authorization.DataAccess;
using DesignDocu.Authorization.DataAccess.Models;
using DesignDocu.Authorization.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DesignDocu.Authorization;

public static class ServiceExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var authConfiguration = configuration.GetSection("Auth").Get<AuthConfiguration>()
                                ?? throw new ArgumentNullException(nameof(AuthConfiguration), 
                                    "Auth section not found in appsettings.json");
        services.AddSingleton(authConfiguration);

        services.AddScoped<ITokenProvider, JwtTokenProvider>();
        
        services.AddDbContext<AuthorizationContext>(options =>
        {
            options.UseSqlServer(authConfiguration.ConnectionString);
        });
        
        services.AddIdentityApiEndpoints<ApplicationUser>()
            .AddEntityFrameworkStores<AuthorizationContext>()
            .AddDefaultTokenProviders();

        var key = Encoding.ASCII.GetBytes(authConfiguration.JwtSettings.SigningKey);
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
                    ValidIssuer = authConfiguration.JwtSettings.Issuer,
                    ValidAudience = authConfiguration.JwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        
        services.AddAuthorization();

        return services;
    }
    
    public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        
        return app;
    }
}