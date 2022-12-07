using ContactService.Entities.Contact;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactService.Repository.Interfaces
{
    public interface IContactInfoTypeRepository
    {
        Task<IEnumerable<ContactInfoType>> GetContactInfoTypesAsync();
    }
}