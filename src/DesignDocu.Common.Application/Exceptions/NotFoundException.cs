namespace DesignDocu.Common.Application.Exceptions;

public class NotFoundException(string type, string id) : Exception($"{type} not found for '{id}'");