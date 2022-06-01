using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Api.Models;

[DataContract(Namespace = "http://www.example.com/posts")]
public record Contact
{
    [DataMember]
    [JsonPropertyName("id")]
    public int? Id { get; init; }
    
    [Required]
    [DataMember(IsRequired = true)]
    [JsonPropertyName("surname")]
    public string? Surname { get; init; }
    
    [Required]
    [DataMember(IsRequired = true)]
    [JsonPropertyName("name")]
    public string? Name { get; init; }
    
    [Required]
    [DataMember(IsRequired = true)]
    [JsonPropertyName("number")]
    public string? Number { get; init; }
    
    [Required]
    [DataMember(IsRequired = true)]
    [JsonPropertyName("email")]
    public string? Email { get; init; }
}