using DesignDocu.Common.Application.Commands;

namespace DesignDocu.Users.Application.Users.Register;

public class RegisterCommand : ICommand<string>
{
    public string Username { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Password { get; set; } = null!;
}