namespace ContactService.DTOs
{
    public class ContactInfoDTO
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int ContactInfoTypeId { get; set; }
        public string Value { get; set; }
    }
}