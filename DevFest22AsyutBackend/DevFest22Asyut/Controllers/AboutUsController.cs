using DevFest22Asyut.Models;
using DevFest22Asyut.Services;
using DevFest22Asyut.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DevFest22Asyut.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AboutUsController : Controller
    {
        private readonly IGenericService<About> _aboutService;

        public AboutUsController(IGenericService<About> aboutService)
        {
            _aboutService = aboutService;
        }

        public IActionResult Index()
        {
            //List<AboutViewModel> viewModels = new();

            var abouts = _aboutService.GetAll().Select(a => new AboutViewModel() { Id=a.Id,Description=a.Description,Title=a.Title});

            //foreach(var about in abouts)
            //{
            //    AboutViewModel viewModel = new()
            //    {
            //        Id = about.Id,
            //        Title = about.Title,
            //        Description = about.Description
            //    };

            //    viewModels.Add(viewModel);
            //}

            return View(abouts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AboutViewModel aboutVM)
        {
            if (!ModelState.IsValid)
                return View(aboutVM);

            About about = new()
            {
                Title = aboutVM.Title,
                Description = aboutVM.Description
            };

            _aboutService.Insert(about);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if(string.IsNullOrWhiteSpace(id))
                return NotFound();

            var about = _aboutService.GetById(id);

            if (about is null)
                return NotFound();

            AboutViewModel aboutViewModel = new()
            {
                Id =about.Id,
                Title = about.Title,
                Description = about.Description
            };

            return View(aboutViewModel);
        }

        [HttpPost]
        public IActionResult Edit(AboutViewModel aboutVM)
        {
            if (!ModelState.IsValid)
                return View(aboutVM);

            if (string.IsNullOrWhiteSpace(aboutVM.Id))
                return NotFound();

            var aboutInDb = _aboutService.GetById(aboutVM.Id);

            if (aboutInDb is null)
                return NotFound();

            aboutInDb.Title = aboutVM.Title;
            aboutInDb.Description = aboutVM.Description;

            _aboutService.Update(aboutInDb);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var aboutInDb = _aboutService.GetById(id);

            if (aboutInDb is null)
                return NotFound();

            _aboutService.Delete(aboutInDb);

            return RedirectToAction(nameof(Index));
        }

    }
}
