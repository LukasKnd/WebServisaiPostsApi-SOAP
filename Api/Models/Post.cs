namespace Api.Models;

public record Post
{
    public int Id { get; init; }
    
    public string Title { get; init; } = null!;

    public string Content { get; init; } = null!;
}