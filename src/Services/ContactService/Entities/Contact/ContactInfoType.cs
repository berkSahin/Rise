using System.Collections.Generic;

namespace ContactService.Entities.Contact
{
    public class ContactInfoType : BaseEntity
    {
        public string Value { get; set; }
        public virtual ICollection<ContactInfo> ContactInfos { get; set; }
    }
}