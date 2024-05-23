using DesignDocu.Authorization.DataAccess.Models;

namespace DesignDocu.Authorization.Tokens;

public interface ITokenProvider
{
    string GenerateAccessToken(ApplicationUser user);
    
    string GenerateRefreshToken();
}