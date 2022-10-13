using DevFest22Asyut.Models;
using DevFest22Asyut.Services;
using DevFest22Asyut.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DevFest22Asyut.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        private readonly IGenericService<ContactInfo> _contactInfoService;
        private readonly IGenericService<Contact> _contactMessagesService;


        public ContactController(
            IGenericService<Contact> contactMessagesServics, 
            IGenericService<ContactInfo> contactInfoService
            )
        {
            _contactMessagesService = contactMessagesServics;
            _contactInfoService = contactInfoService;
        }

        public IActionResult Index()
        {
            ContactInfoViewModel contactInfoViewModel = new();

            var contact = _contactInfoService.GetAll().FirstOrDefault();

            if(contact != null)
            {
                 contactInfoViewModel = new()
                {
                    Id = contact.Id,
                    Email = contact.Email,
                    Facebook = contact.Facebook,
                    LinkedIn = contact.LinkedIn,
                    Twitter = contact.Twitter,
                    Youtube = contact.Youtube
                };
            }

            return View(contactInfoViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ContactInfoViewModel contactVM)
        {
            if (!ModelState.IsValid)
                return View(contactVM);

            ContactInfo contact = new()
            {
                Email= contactVM.Email,
                Facebook= contactVM.Facebook,
                LinkedIn= contactVM.LinkedIn,
                Twitter= contactVM.Twitter,
                Youtube = contactVM.Youtube        
            };

            _contactInfoService.Insert(contact);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var contact =  _contactInfoService.GetById(id);

            if (contact == null)
                return NotFound();

            ContactInfoViewModel contactInfoViewModel = new()
            {
                Id =contact.Id, 
                Email =contact.Email,
                Facebook =contact.Facebook,
                LinkedIn =contact.LinkedIn,
                Twitter = contact.Twitter,
                Youtube = contact.Youtube
            };

            return View(contactInfoViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ContactInfoViewModel contactVM)
        {
            if (!ModelState.IsValid)
                return View(contactVM);

            if(string.IsNullOrWhiteSpace(contactVM.Id))
                return View(contactVM);

            var contactInDb = _contactInfoService.GetById(contactVM.Id);

            if (contactInDb is null)
                return NotFound();

            contactInDb.Email = contactVM.Email;
            contactInDb.Facebook = contactVM.Facebook;
            contactInDb.LinkedIn = contactVM.LinkedIn;
            contactInDb.Twitter = contactVM.Twitter;
            contactInDb.Youtube = contactVM.Youtube;

            _contactInfoService.Update(contactInDb);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var contactInfoInDb = _contactInfoService.GetById(id);

            if (contactInfoInDb is null)
                return NotFound();

            _contactInfoService.Delete(contactInfoInDb);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Messages()
        {
            List<MessageViewModel> messagesViewModel = new();

            var messages = _contactMessagesService.GetAll();

            if (messages.Any())
            {
                foreach(var message in messages)
                {
                    MessageViewModel viewModel = new()
                    {
                        Id = message.Id,
                        Email = message.Email,
                        Name = message.Name,
                        PhoneNumber = message.PhoneNumber,
                        Message = message.Message
                    };

                    messagesViewModel.Add(viewModel);
                }
            }

            return View(messagesViewModel);
        }


        [HttpPost]
        public IActionResult DeleteMessage(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var message = _contactMessagesService.GetById(id);

            if (message is null)
                return NotFound();

            _contactMessagesService.Delete(message);

            return RedirectToAction(nameof(Index));
        }


    }
}
