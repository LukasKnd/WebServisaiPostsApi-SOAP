namespace Api.Models.Entities;

public class PostEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
}