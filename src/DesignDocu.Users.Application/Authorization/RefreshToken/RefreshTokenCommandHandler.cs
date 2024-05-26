using DesignDocu.Common.Application.Commands;
using DesignDocu.Common.Application.Exceptions;
using DesignDocu.Infrastructure.Authorization.Abstractions.Authorization;
using DesignDocu.Users.Application.Authorization.Login;
using DessignDocu.Users.Domain.Users.Entities;
using DessignDocu.Users.Domain.Users.Repositories;

namespace DesignDocu.Users.Application.Authorization.RefreshToken;

public class RefreshTokenCommandHandler(IUserRepository userRepository, IAuthorizationService authorizationService) : ICommandHandler<RefreshTokenCommand, UserTokenResponse>
{
    public async Task<UserTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUsernameAsync(request.Username);
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Username);
        }
        
        var storedRefreshToken = await authorizationService.GetRefreshTokenAsync(user.Id);
        if (storedRefreshToken != request.RefreshToken)
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