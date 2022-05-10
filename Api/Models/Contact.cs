using System.Text.Json.Serialization;

namespace Api.Models;

public record Contact
{
    [JsonPropertyName("id")]
    public int? Id { get; init; }
    
    [JsonPropertyName("surname")]
    public string? Surname { get; init; }
    
    [JsonPropertyName("name")]
    public string? Name { get; init; }
    
    [JsonPropertyName("number")]
    public string? Number { get; init; }
    
    [JsonPropertyName("email")]
    public string? Email { get; init; }
}