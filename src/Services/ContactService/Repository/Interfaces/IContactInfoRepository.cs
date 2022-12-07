using ContactService.Entities.Contact;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactService.Repository.Interfaces
{
    public interface IContactInfoRepository
    {
        Task<bool> ContactInfoExistAsync(int contactInfoId);

        Task<bool> AddContactInfoAsync(ContactInfo contactInfo);

        Task<bool> DeleteContactInfoAsync(ContactInfo contactInfo);

        Task<ContactInfo> GetContactInfoAsync(int contactId);

        Task<IEnumerable<ContactInfo>> GetContactInfosByContactIdAsync(int contactId);

        Task<bool> SaveAsync();
    }
}