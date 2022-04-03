using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public record SavePostRequest
{
    [Required]
    public string Title { get; init; } = null!;

    [Required]
    public string Content { get; init; } = null!;

    public IEnumerable<string>? Tags { get; init; }
}