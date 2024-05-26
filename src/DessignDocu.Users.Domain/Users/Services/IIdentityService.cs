namespace DessignDocu.Users.Domain.Users.Services;

public interface IIdentityService
{
    Task<string> RegisterUserAsync(string userName, string email, string password);
}