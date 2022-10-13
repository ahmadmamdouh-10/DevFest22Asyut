using AutoMapper;
using DevFest22Asyut.Dtos;
using DevFest22Asyut.Errors;
using DevFest22Asyut.Models;
using DevFest22Asyut.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevFest22Asyut.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericService<Article> _blogService;

        public BlogController(IGenericService<Article> blogService, IMapper mapper)
        {
            _blogService = blogService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ArticleDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var articles = _blogService.GetAll();

            var response = _mapper.Map<IEnumerable<ArticleDto>>(articles);

            return Ok(response);
        }

        [HttpGet("{articleId:int}")]
        [ProducesResponseType(typeof(ArticleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string articleId)
        {
            var article = _blogService.GetById(articleId);

            if(article == null)
            {
                return BadRequest(new ApiResponse(404, "Sorry, Could not find this article."));
            }
            var response = _mapper.Map<ArticleDto>(article);

            return Ok(response);
        }

    }
}
