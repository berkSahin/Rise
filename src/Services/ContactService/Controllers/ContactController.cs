using AutoMapper;
using ContactService.DTOs;
using ContactService.Entities.Contact;
using ContactService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        #region Fields

        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        #endregion Fields

        #region Ctor

        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        #endregion Ctor

        #region Methods

        [HttpPost("AddContact")]
        [ProducesResponseType(200, Type = typeof(Contact))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddContact(ContactDTO contact)
        {
            if (contact == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newContact = _mapper.Map<Contact>(contact);

            if (!(await _contactService.AddContactAsync(newContact)))
            {
                return StatusCode(500);
            }

            return Ok(_mapper.Map<ContactDTO>(newContact));
        }

        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpDelete("DeleteContact/{contactId}")]
        public async Task<IActionResult> DeleteContact(int contactId)
        {
            if (!(await _contactService.ContactExistAsync(contactId)))
            {
                return NotFound();
            }

            var contact = await _contactService.GetContactAsync(contactId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!(await _contactService.DeleteContactAsync(contact)))
            {
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpGet("GetContacts")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Contact>))]
        public async Task<IActionResult> GetContacts()
        {
            var contacts = _mapper.Map<List<ContactDTO>>(await _contactService.GetContactsAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(contacts);
        }

        [HttpGet("GetContact/{contactId}")]
        [ProducesResponseType(200, Type = typeof(Contact))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetContact(int contactId)
        {
            if (!(await _contactService.ContactExistAsync(contactId)))
                return NotFound();

            var contact = _mapper.Map<ContactDTO>(await _contactService.GetContactAsync(contactId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(contact);
        }

        [HttpGet("GetContactsWithInfos")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Contact>))]
        public async Task<IActionResult> GetContactsWithInfos()
        {
            var contacts = _mapper.Map<List<ContactDetailsDTO>>(await _contactService.GetContactsAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(contacts);
        }

        [HttpGet("GetContactWithInfos/{contactId}")]
        [ProducesResponseType(200, Type = typeof(Contact))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetContactWithInfos(int contactId)
        {
            if (!(await _contactService.ContactExistAsync(contactId)))
                return NotFound();

            var contact = _mapper.Map<ContactDetailsDTO>(await _contactService.GetContactAsync(contactId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(contact);
        }

        [HttpPost("AddContactInfo")]
        [ProducesResponseType(200, Type = typeof(ContactInfo))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddContactInfo(AddContactInfoDTO contactInfo)
        {
            if (contactInfo == null)
                return BadRequest(ModelState);

            if (!(await _contactService.ContactExistAsync(contactInfo.ContactId)))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newContactInfo = new ContactInfo
            {
                ContactId = contactInfo.ContactId,
                ContactInfoTypeId = contactInfo.ContactInfoTypeId,
                Value = contactInfo.Value
            };

            if (!(await _contactService.AddContactIfoAsync(newContactInfo)))
            {
                return StatusCode(500);
            }

            return Ok(_mapper.Map<AddContactInfoDTO>(newContactInfo));
        }

        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpDelete("DeleteContactInfo/{contactInfoId}")]
        public async Task<IActionResult> DeleteContactInfo(int contactInfoId)
        {
            if (!(await _contactService.ContactInfoExistAsync(contactInfoId)))
            {
                return NotFound();
            }

            var contactInfo = await _contactService.GetContactInfoAsync(contactInfoId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!(await _contactService.DeleteContactInfoAsync(contactInfo)))
            {
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpGet("GetContactInfoTypes")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ContactInfoType>))]
        public async Task<IActionResult> GetContactInfoTypes()
        {
            var contactInfoTypes = _mapper.Map<List<ContactInfoTypeDTO>>(await _contactService.GetContactInfoTypesAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(contactInfoTypes);
        }

        [HttpGet("GetContactInfosByContactId/{contactId}")]
        [ProducesResponseType(200, Type = typeof(Contact))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetContactInfosByContactId(int contactId)
        {
            if (!(await _contactService.ContactExistAsync(contactId)))
                return NotFound();

            var contactInfos = _mapper.Map<List<ContactInfoDTO>>(await _contactService.GetContactInfosByContactIdAsync(contactId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(contactInfos);
        }

        #endregion Methods
    }
}