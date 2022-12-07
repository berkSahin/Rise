using ContactService.Entities.Contact;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactService.Repository.Interfaces
{
    public interface IContactRepository
    {
        Task<bool> ContactExistAsync(int contactId);

        Task<bool> AddContactAsync(Contact contact);

        Task<bool> DeleteContactAsync(Contact contact);

        Task<Contact> GetContactAsync(int contactId);

        Task<IEnumerable<Contact>> GetContactsAsync();

        Task<bool> SaveAsync();
    }
}