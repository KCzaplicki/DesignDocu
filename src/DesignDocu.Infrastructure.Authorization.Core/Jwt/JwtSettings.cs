namespace DesignDocu.Infrastructure.Auth.Authorization.Core.Jwt;

internal class JwtSettings
{
    public string SigningKey { get; init; }

    public string Issuer { get; init; }

    public string Audience { get; init; }
}