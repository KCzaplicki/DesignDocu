using DesignDocu.Common.Application.Commands;

namespace DesignDocu.Users.Application.Authorization.Login;

public class LoginCommand : ICommand<UserTokenResponse>
{
    public string Username { get; init; }
    public string Password { get; init; }
}