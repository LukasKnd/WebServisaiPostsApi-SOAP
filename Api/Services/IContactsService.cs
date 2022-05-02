using Api.Models;

namespace Api.Services;

public interface IContactsService
{
    Task<Contact> GetContact(int id);
}