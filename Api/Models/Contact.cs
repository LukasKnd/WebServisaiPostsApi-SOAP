using System.Runtime.Serialization;

namespace Api.Models;

[DataContract(Namespace = "http://www.example.com/posts")]
public record Contact
{
    [DataMember]
    public int? Id { get; init; }
    
    [DataMember]
    public string? Surname { get; init; }
    
    [DataMember]
    public string? Name { get; init; }
    
    [DataMember]
    public string? Number { get; init; }
    
    [DataMember]
    public string? Email { get; init; }
}