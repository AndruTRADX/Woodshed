namespace Woodshed.Application.Exceptions;

public class ForbiddenException(string message = "")
    : ApplicationException(string.IsNullOrWhiteSpace(message) ? "You do not have permission to perform this action." : message)
{

}