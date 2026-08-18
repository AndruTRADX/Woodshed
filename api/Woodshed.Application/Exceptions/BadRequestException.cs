namespace Woodshed.Application.Exceptions;

public class BadRequestException(string message = "") : ApplicationException(string.IsNullOrWhiteSpace(message) ? "Bad request" : message)
{

}