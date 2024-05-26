using DessignDocu.Users.Domain.Users.Entities;

namespace DessignDocu.Users.Domain.Users.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task<string> AddAsync(UserWithPassword user);
}