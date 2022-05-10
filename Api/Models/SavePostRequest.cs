using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public record SavePostRequest : IWithContactRequest
{
    [Required]
    public string Title { get; init; } = null!;

    [Required]
    public string Content { get; init; } = null!;

    public IEnumerable<string>? Tags { get; init; }
    
    public int? ContactId { get; init; }
    
    public Contact? Contact { get; init; }
}