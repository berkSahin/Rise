using ContactService.Data;
using ContactService.Entities.Contact;
using ContactService.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactService.Repository
{
    public class ContactInfoTypeRepository : IContactInfoTypeRepository
    {
        #region Fields

        private readonly ContactDbContext _context;

        #endregion Fields

        #region Ctor

        public ContactInfoTypeRepository(ContactDbContext context)
        {
            _context = context;
        }

        #endregion Ctor

        #region Methods

        public async Task<IEnumerable<ContactInfoType>> GetContactInfoTypesAsync()
        {
            return await _context.ContactInfoTypes.ToListAsync<ContactInfoType>();
        }

        #endregion Methods
    }
}