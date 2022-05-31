using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Api.Models;

[DataContract(Namespace = "http://www.example.com/posts")]
public record SavePostRequest : IWithContactRequest
{
    [Required]
    [DataMember(IsRequired = true)]
    public string Title { get; init; } = null!;

    [Required]
    [DataMember(IsRequired = true)]
    public string Content { get; init; } = null!;

    [DataMember]
    public IEnumerable<string>? Tags { get; init; }
    
    [DataMember]
    public int? ContactId { get; init; }
    
    [DataMember]
    public Contact? Contact { get; init; }
}