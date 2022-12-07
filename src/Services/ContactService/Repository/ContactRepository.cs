using ContactService.Data;
using ContactService.Entities.Contact;
using ContactService.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactService.Repository
{
    public class ContactRepository : IContactRepository
    {
        #region Fields

        private readonly ContactDbContext _context;

        #endregion Fields

        #region Ctor

        public ContactRepository(ContactDbContext context)
        {
            _context = context;
        }

        #endregion Ctor

        #region Methods

        public async Task<bool> ContactExistAsync(int contactId)
        {
            return await _context.Contacts.AnyAsync(a => a.Id == contactId);
        }

        public async Task<bool> AddContactAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            return await SaveAsync();
        }

        public async Task<bool> DeleteContactAsync(Contact contact)
        {
            _context.Contacts.Remove(contact);
            return await SaveAsync();
        }

        public async Task<Contact> GetContactAsync(int contactId)
        {
            return await _context.Contacts.FindAsync(contactId);
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            return await _context.Contacts.ToListAsync<Contact>();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        #endregion Methods
    }
}