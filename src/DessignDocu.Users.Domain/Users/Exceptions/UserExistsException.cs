namespace DessignDocu.Users.Domain.Users.Exceptions;

public class UserExistsException(string email) : Exception($"User with email '{email}' already exists.");