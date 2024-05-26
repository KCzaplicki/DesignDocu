using DesignDocu.Common.Application.Commands;
using DesignDocu.Users.Application.Authorization.Login;

namespace DesignDocu.Users.Application.Authorization.RefreshToken;

public class RefreshTokenCommand : ICommand<UserTokenResponse>
{
    public string Username { get; init; }
    public string RefreshToken { get; init; }
}