using System.ComponentModel.DataAnnotations;

namespace ContactService.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}