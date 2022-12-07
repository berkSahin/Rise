using System.Collections.Generic;

namespace ContactService.Entities.Contact
{
    public class Contact : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public virtual ICollection<ContactInfo> ContactInfos { get; set; }
    }
}