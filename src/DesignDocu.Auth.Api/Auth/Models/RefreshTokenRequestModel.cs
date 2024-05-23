namespace DesignDocu.Auth.Api.Auth.Models;

public class RefreshTokenRequestModel
{
    public string Username { get; init; }
    public string RefreshToken { get; init; }
}