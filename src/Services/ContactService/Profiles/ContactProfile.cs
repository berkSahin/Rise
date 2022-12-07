using AutoMapper;
using ContactService.DTOs;
using ContactService.Entities.Contact;

namespace ContactService.Profiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactDTO>()
                .ReverseMap();
            CreateMap<ContactInfo, ContactInfoDTO>();
            CreateMap<ContactInfo, ContactDetailsInfoDTO>();
            CreateMap<ContactInfoType, ContactInfoTypeDTO>();
            CreateMap<Contact, ContactDetailsDTO>();
        }
    }
}