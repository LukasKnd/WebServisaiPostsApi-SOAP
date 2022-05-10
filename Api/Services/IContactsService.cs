using Api.Models;

namespace Api.Services;

public interface IContactsService
{
    Task<Contact> GetContact(int id);

    Task<Contact> CreateContact(Contact contact);

    Task<Contact> UpdateContact(Contact contact);
}