namespace DesignDocu.Authorization;

public class AuthConfiguration
{
    public JwtSettings JwtSettings { get; init; }

    public string ConnectionString { get; init; }
}

public class JwtSettings
{
    public string SigningKey { get; init; }

    public string Issuer { get; init; }

    public string Audience { get; init; }
}