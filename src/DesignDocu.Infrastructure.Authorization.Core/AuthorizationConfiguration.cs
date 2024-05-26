using DesignDocu.Infrastructure.Auth.Authorization.Core.Jwt;

namespace DesignDocu.Infrastructure.Auth.Authorization.Core;

internal class AuthorizationConfiguration
{
    public string ConnectionString { get; init; }
    public JwtSettings JwtSettings { get; init; }
}