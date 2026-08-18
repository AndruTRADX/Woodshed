namespace Woodshed.Domain.Common;

public class DomainException(string message = "") 
    : ApplicationException(string.IsNullOrWhiteSpace(message) ? "You cannot perform this action." : message)
{

}
