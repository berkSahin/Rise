using ContactService.Entities.Contact;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactService.Services.Interfaces
{
    public interface IContactService
    {
        Task<bool> ContactExistAsync(int contactId);

        Task<bool> AddContactAsync(Contact contact);

        Task<bool> DeleteContactAsync(Contact contact);

        Task<Contact> GetContactAsync(int contactId);

        Task<IEnumerable<Contact>> GetContactsAsync();

        Task<bool> ContactInfoExistAsync(int contactInfoId);

        Task<bool> AddContactIfoAsync(ContactInfo contactInfo);

        Task<bool> DeleteContactInfoAsync(ContactInfo contactInfo);

        Task<ContactInfo> GetContactInfoAsync(int contactInfoId);

        Task<IEnumerable<ContactInfo>> GetContactInfosByContactIdAsync(int contactId);

        Task<IEnumerable<ContactInfoType>> GetContactInfoTypesAsync();
    }
}