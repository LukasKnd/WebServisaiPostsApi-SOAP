namespace Api.Models;

public record SavePostRequest
{
    public string Title { get; init; } = null!;

    public string Content { get; init; } = null!;
}