using System.Runtime.Serialization;

namespace Api.Models;

[DataContract(Namespace = "http://www.example.com/posts")]
public record Post
{
    [DataMember]
    public int Id { get; init; }
    
    [DataMember]
    public Contact? Contact { get; init; }

    [DataMember]
    public string Title { get; init; } = null!;

    [DataMember]
    public string Content { get; init; } = null!;

    [DataMember]
    public IEnumerable<string> Tags { get; init; } = null!;

    [DataMember]
    public DateTime Created { get; init; }

    [DataMember]
    public DateTime Updated { get; init; }
}