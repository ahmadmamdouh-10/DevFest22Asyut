using DevFest22Asyut.Data;
using DevFest22Asyut.Helpers;
using DevFest22Asyut.Models;
using DevFest22Asyut.Services;
using DevFest22Asyut.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace DevFest22Asyut.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private readonly IGenericService<Article> _blogService;
        private readonly IHostingEnvironment _environment;


        public BlogController(
            IGenericService<Article> blogService,
            IHostingEnvironment environment
            )
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            var articles = _blogService.GetAll()
                .Select(article => new ArticleViewModel()
                {
                    Id = article.Id,
                    Title = article.Title,
                    Description = article.Description,
                    Image = article.Image
                });

            return View(articles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateArticleViewModel articleVM)
        {
            if (!ModelState.IsValid)
                return View(articleVM);

            var imagePath = string.Empty;

            if (articleVM.Image != null)
                 imagePath = await SaveFile.Save(articleVM.Image, _environment);

            if(!string.IsNullOrWhiteSpace(articleVM.Title) && !string.IsNullOrWhiteSpace(articleVM.Description))
            {
                var article = new Article(articleVM.Title, imagePath, articleVM.Description);

                _blogService.Insert(article);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var article = _blogService.GetById(id);


            if (article == null)
                return NotFound();

            var articleVM = new CreateArticleViewModel()
            {
                Title = article.Title,
                Description = article.Description,
                Id = article.Id
            };

            return View(articleVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreateArticleViewModel articleVM)
        {
            if (!ModelState.IsValid)
                return View(articleVM);

            if (string.IsNullOrWhiteSpace(articleVM.Id))
                return NotFound();

            var article = _blogService.GetById(articleVM.Id);

            if (article is null)
                return NotFound();

            if(articleVM.Image != null)
            {
                var imagePath = await SaveFile.Save(articleVM.Image, _environment);
                article.Image = string.IsNullOrEmpty(imagePath) ? article.Image : imagePath;
            }

            article.Title = articleVM.Title;
            article.Description = articleVM.Description;

            _blogService.Update(article);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var article = _blogService.GetById(id);

            if (article is null)
                return NotFound();

            _blogService.Delete(article);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
