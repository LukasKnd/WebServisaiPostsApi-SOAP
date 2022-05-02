using System.Net;
using System.Text.Json;
using Api.Exceptions;
using Api.Models;

namespace Api.Services;

public class ContactsService : IContactsService
{
    private readonly HttpClient _client;

    public ContactsService(HttpClient client)
    {
        _client = client;
    }

    public async Task<Contact> GetContact(int id)
    {
        var response = await _client.GetAsync($"contacts/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new ContactNotFoundException();
        }

        response.EnsureSuccessStatusCode();

        var stringContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Contact>(stringContent)!;
    }
}