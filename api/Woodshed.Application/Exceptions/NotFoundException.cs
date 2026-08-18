namespace Woodshed.Application.Exceptions;

public class NotFoundException(string name, object key) : ApplicationException($"Entity \"{name}\" ({key}) has not been found")
{
    
}
