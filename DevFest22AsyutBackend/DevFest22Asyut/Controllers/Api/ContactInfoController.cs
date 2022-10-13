using AutoMapper;
using DevFest22Asyut.Dtos;
using DevFest22Asyut.Errors;
using DevFest22Asyut.Models;
using DevFest22Asyut.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevFest22Asyut.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfoController : ControllerBase
    {
        private readonly IGenericService<Contact> _ContactService;
        private readonly IGenericService<ContactInfo> _ContactInfoService;
        private readonly IMapper _mapper;

        public ContactInfoController(
     IGenericService<Contact> ContactService,
     IGenericService<ContactInfo> ContactInfoService,
     IMapper mapper
     )
        {
            _ContactService = ContactService;
            _ContactInfoService = ContactInfoService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ContactInfoDto), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var contactInfo = _ContactInfoService.GetAll().FirstOrDefault();

            var response = _mapper.Map<ContactInfoDto>(contactInfo);

            return Ok(response);
        }

        [HttpPost("NewMessage")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] Contact contact)
        {     
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400, "Sorry, Couldn't send your message right now. Try again later."));

            _ContactService.Insert(contact);

            return Ok(new ApiResponse(200, "Sent Successfully"));
        }
    }
}
