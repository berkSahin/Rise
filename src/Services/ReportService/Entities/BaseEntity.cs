using System.ComponentModel.DataAnnotations;

namespace ReportService.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}