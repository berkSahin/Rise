namespace ContactService.DTOs
{
    public class AddContactInfoDTO
    {
        public int ContactId { get; set; }
        public int ContactInfoTypeId { get; set; }
        public string Value { get; set; }
    }
}