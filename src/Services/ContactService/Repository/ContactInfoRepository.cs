using ContactService.Data;
using ContactService.Entities.Contact;
using ContactService.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Repository
{
    public class ContactInfoRepository : IContactInfoRepository
    {
        #region Fields

        private readonly ContactDbContext _context;

        #endregion Fields

        #region Ctor

        public ContactInfoRepository(ContactDbContext context)
        {
            _context = context;
        }

        #endregion Ctor

        #region Methods

        public async Task<bool> ContactInfoExistAsync(int contactInfoId)
        {
            return await _context.ContactInfos.AnyAsync(a => a.Id == contactInfoId);
        }

        public async Task<bool> AddContactInfoAsync(ContactInfo contactInfo)
        {
            await _context.ContactInfos.AddAsync(contactInfo);
            return await SaveAsync();
        }

        public async Task<bool> DeleteContactInfoAsync(ContactInfo contactInfo)
        {
            _context.ContactInfos.Remove(contactInfo);
            return await SaveAsync();
        }

        public async Task<ContactInfo> GetContactInfoAsync(int contactInfoId)
        {
            return await _context.ContactInfos.FindAsync(contactInfoId);
        }

        public async Task<IEnumerable<ContactInfo>> GetContactInfosByContactIdAsync(int contactId)
        {
            return await _context.ContactInfos.Where(a => a.ContactId == contactId).ToListAsync<ContactInfo>();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        #endregion Methods
    }
}