using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Api.Exceptions;
using Api.Models;

namespace Api.Services;

public class ContactsService : IContactsService
{
    private static int _newid = 1;
    private readonly HttpClient _client;
    
    public ContactsService(HttpClient client)
    {
        _client = client;
    }

    public async Task<Contact> GetContact(int id)
    {
        return new Contact();
        var response = await _client.GetAsync($"contacts/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new ContactNotFoundException();
        }

        response.EnsureSuccessStatusCode();

        var stringContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Contact>(stringContent)!;
    }

    public async Task<Contact> CreateContact(Contact contact)
    {
        return new Contact();
        contact = contact with { Id = _newid++ };
        
        var serialized = JsonSerializer.Serialize(contact);
        
        var response = await _client.PostAsync($"contacts", new StringContent(serialized, Encoding.Default, MediaTypeNames.Application.Json));

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new ContactBadRequestException(await response.Content.ReadAsStringAsync());
        }

        response.EnsureSuccessStatusCode();

        return contact;
    }
    
    public async Task<Contact> UpdateContact(Contact contact)
    {
        return new Contact();
        
        var serialized = JsonSerializer.Serialize(contact with{ Id = null });
        
        var response = await _client.PutAsync($"contacts/{contact.Id}", new StringContent(serialized, Encoding.Default, MediaTypeNames.Application.Json));

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new ContactBadRequestException(await response.Content.ReadAsStringAsync());
        }
        
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new ContactNotFoundException();
        }

        response.EnsureSuccessStatusCode();
        
        return contact;
    }
    
}