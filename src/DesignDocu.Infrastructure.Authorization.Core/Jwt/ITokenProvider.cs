using DesignDocu.Infrastructure.Authorization.Abstractions.DataAccess;

namespace DesignDocu.Infrastructure.Auth.Authorization.Core.Jwt;

internal interface ITokenProvider
{
    string GenerateAccessToken(ApplicationUser applicationUser);
    
    string GenerateRefreshToken();
}