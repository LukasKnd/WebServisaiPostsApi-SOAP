namespace Api.Models;

public interface IWithContactRequest
{
    public int? ContactId { get; init; }
    public Contact? Contact { get; init; }
}