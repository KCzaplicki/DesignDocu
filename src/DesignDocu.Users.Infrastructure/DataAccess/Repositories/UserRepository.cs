using DessignDocu.Users.Domain.Users.Entities;
using DessignDocu.Users.Domain.Users.Repositories;
using DessignDocu.Users.Domain.Users.Services;
using Microsoft.EntityFrameworkCore;

namespace DesignDocu.Users.Infrastructure.DataAccess.Repositories;

public class UserRepository(UsersContext context, IIdentityService identityService) : IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            return null;
        }
        
        return new User
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
        };
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        if (user == null)
        {
            return null;
        }
        
        return new User
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
        };
    }

    public async Task<string> AddAsync(UserWithPassword user)
    {
        return await identityService.RegisterUserAsync(user.UserName, user.Email, user.Password);
    }
}