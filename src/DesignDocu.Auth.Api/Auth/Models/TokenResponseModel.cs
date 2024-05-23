namespace DesignDocu.Auth.Api.Auth.Models;

public class TokenResponseModel
{
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
}