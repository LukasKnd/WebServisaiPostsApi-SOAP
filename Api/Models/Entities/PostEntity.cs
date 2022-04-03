namespace Api.Models.Entities;

public class PostEntity : BaseEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string TagsJson { get; set; } = null!;
}