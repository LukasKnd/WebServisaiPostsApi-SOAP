namespace Api.Models;

public class PatchPostRequest : IWithContactRequest
{
    public string? Title { get; init; } = null!;

    public string? Content { get; init; } = null!;

    public IEnumerable<string>? Tags { get; init; }
    
    public int? ContactId { get; init; }
    
    public Contact? Contact { get; init; }
}