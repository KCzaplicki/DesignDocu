using DesignDocu.Infrastructure.Authorization.Abstractions.DataAccess;
using DessignDocu.Users.Domain.Users.Services;
using Microsoft.AspNetCore.Identity;

namespace DesignDocu.Users.Infrastructure.Identity;

public class IdentityService(UserManager<ApplicationUser> userManager) 
    : IIdentityService
{
    public async Task<string> RegisterUserAsync(string userName, string email, string password)
    {
        var user = new ApplicationUser
        {
            UserName = userName,
            Email = email
        };

        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            return user.Id;
        }

        throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
    }
}