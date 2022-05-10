namespace Api.Exceptions;

public class ContactBadRequestException: Exception
{
    public ContactBadRequestException(string message): base(message) {}
}