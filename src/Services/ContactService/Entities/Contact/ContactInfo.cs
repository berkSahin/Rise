using System.ComponentModel.DataAnnotations.Schema;

namespace ContactService.Entities.Contact
{
    public class ContactInfo : BaseEntity
    {
        [ForeignKey("Contact")]
        public int ContactId { get; set; }

        [ForeignKey("ContactInfoType")]
        public int ContactInfoTypeId { get; set; }

        public string Value { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual ContactInfoType ContactInfoType { get; set; }
    }
}