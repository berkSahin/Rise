using ContactService.Entities.Contact;
using ContactService.Repository.Interfaces;
using ContactService.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactService.Services
{
    public class ContactService : IContactService
    {
        #region Fields

        private readonly IContactRepository _contactRepository;
        private readonly IContactInfoRepository _contactInfoRepository;
        private readonly IContactInfoTypeRepository _contactInfoTypeRepository;

        #endregion Fields

        #region Ctor

        public ContactService(IContactRepository contactRepository, IContactInfoRepository contactInfoRepository, IContactInfoTypeRepository contactInfoTypeRepository)
        {
            _contactRepository = contactRepository;
            _contactInfoRepository = contactInfoRepository;
            _contactInfoTypeRepository = contactInfoTypeRepository;
        }

        #endregion Ctor

        #region Methods

        public async Task<bool> ContactExistAsync(int contactId)
        {
            return await _contactRepository.ContactExistAsync(contactId);
        }

        public async Task<bool> AddContactAsync(Contact contact)
        {
            return await _contactRepository.AddContactAsync(contact);
        }

        public async Task<bool> DeleteContactAsync(Contact contact)
        {
            return await _contactRepository.DeleteContactAsync(contact);
        }

        public async Task<Contact> GetContactAsync(int contactId)
        {
            return await _contactRepository.GetContactAsync(contactId);
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            return await _contactRepository.GetContactsAsync();
        }

        public async Task<bool> ContactInfoExistAsync(int contactInfoId)
        {
            return await _contactInfoRepository.ContactInfoExistAsync(contactInfoId);
        }

        public async Task<bool> AddContactIfoAsync(ContactInfo contactInfo)
        {
            return await _contactInfoRepository.AddContactInfoAsync(contactInfo);
        }

        public async Task<bool> DeleteContactInfoAsync(ContactInfo contactInfo)
        {
            return await _contactInfoRepository.DeleteContactInfoAsync(contactInfo);
        }

        public async Task<ContactInfo> GetContactInfoAsync(int contactInfoId)
        {
            return await _contactInfoRepository.GetContactInfoAsync(contactInfoId);
        }

        public async Task<IEnumerable<ContactInfoType>> GetContactInfoTypesAsync()
        {
            return await _contactInfoTypeRepository.GetContactInfoTypesAsync();
        }

        public async Task<IEnumerable<ContactInfo>> GetContactInfosByContactIdAsync(int contactId)
        {
            return await _contactInfoRepository.GetContactInfosByContactIdAsync(contactId);
        }

        #endregion Methods
    }
}