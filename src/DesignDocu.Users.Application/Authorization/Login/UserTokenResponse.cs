namespace DesignDocu.Users.Application.Authorization.Login;

public class UserTokenResponse
{
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
}