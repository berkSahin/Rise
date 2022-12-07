using System.Collections.Generic;

namespace ReportService.DTOs
{
    public class ContactDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public IEnumerable<ContactInfoDTO> ContactInfos { get; set; }
    }

    public class ContactInfoDTO
    {
        public int Id { get; set; }
        public int ContactInfoTypeId { get; set; }
        public string Value { get; set; }
    }
}