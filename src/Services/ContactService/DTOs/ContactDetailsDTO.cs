using System.Collections.Generic;

namespace ContactService.DTOs
{
    public class ContactDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public IEnumerable<ContactDetailsInfoDTO> ContactInfos { get; set; }
    }

    public class ContactDetailsInfoDTO
    {
        public int Id { get; set; }
        public int ContactInfoTypeId { get; set; }
        public string Value { get; set; }
    }
}