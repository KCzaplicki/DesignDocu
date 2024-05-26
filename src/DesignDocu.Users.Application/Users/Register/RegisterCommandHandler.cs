using DesignDocu.Common.Application.Commands;
using DessignDocu.Users.Domain.Users.Entities;
using DessignDocu.Users.Domain.Users.Exceptions;
using DessignDocu.Users.Domain.Users.Repositories;

namespace DesignDocu.Users.Application.Users.Register;

public class RegisterCommandHandler(IUserRepository userRepository) 
    : ICommandHandler<RegisterCommand, string>
{
    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            throw new UserExistsException(request.Email);
        }

        var user = new UserWithPassword
        {
            UserName = request.Username,
            Email = request.Email,
            Password = request.Password
        };

        return await userRepository.AddAsync(user);
    }
}