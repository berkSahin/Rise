using ReportService.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportService.Clients.Contact
{
    public interface IContactHttpClient
    {
        Task<IEnumerable<ContactDTO>> GetContactsAsync();
    }
}