using DesignDocu.Common.Application.Commands;
using DesignDocu.Common.Application.Exceptions;
using DesignDocu.Infrastructure.Authorization.Abstractions.Authorization;
using DessignDocu.Users.Domain.Users.Repositories;

namespace DesignDocu.Users.Application.Authorization.Login;

public class LoginCommandHandler(IUserRepository userRepository, IAuthorizationService authorizationService) 
    : ICommandHandler<LoginCommand, UserTokenResponse?>
{
    public async Task<UserTokenResponse?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUsernameAsync(request.Username);
        if (user == null || !await authorizationService.CheckPasswordAsync(user.Id, request.Password))
        {
            throw new UnauthorizedException();
        }
        
        var (accessToken, refreshToken) = await authorizationService.GenerateTokenAsync(user.Id);
        await authorizationService.SetRefreshTokenAsync(user.Id, refreshToken);
            
        return new UserTokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

    }
}