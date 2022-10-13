using DevFest22Asyut.Dtos;
using DevFest22Asyut.Models;
using DevFest22Asyut.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevFest22Asyut.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutUsController : ControllerBase
    {
        private readonly IGenericService<About> _aboutService;

        public AboutUsController(IGenericService<About> aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AboutDto>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var aboutList = _aboutService.GetAll().Select(a=> new AboutDto() { Id = a.Id, Title =a.Title, Description = a.Description});

            return Ok(aboutList);
        }
    }
}
