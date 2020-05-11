using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscussionPlatform.Data.Inerfaces;
using DiscussionPlatform.Data.Models;
using DiscussionPlatform.Models.Mail;
using DiscussionPlatform.Models.Platform;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionPlatform.Controllers
{
    public class PlatformController : Controller
    {
        private readonly IPlatform _platformService;
        private readonly IMail _mailService;

        public PlatformController(IPlatform platformService, IMail mailService)
        {
            _platformService = platformService;
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            var platforms = _platformService.GetAll()
                 .Select(platform => new PlatformListingModel
                 {
                     Id = platform.Id,
                     Name = platform.Title,
                     Description = platform.Description
                 });

            var model = new PlatformIndexModel
            {
                PlatformList = platforms
            };

            return View(model);
        }

        public IActionResult Topic(int id, string searchQuery)
        {
            var platform = _platformService.GetById(id);
            var mails = new List<Mail>();

            mails = _mailService.GetFilteredMails(platform, searchQuery).ToList();

            var mailListings = mails.Select(mail => new MailListingModel
            {
                Id = mail.Id,
                AuthorId = mail.User.Id,
                AuthorRating = mail.User.Rating,
                AuthorName = mail.User.UserName,
                Title = mail.Title,
                DateMailed = mail.DateOfCreation.ToString(),
                RepliesCount = mail.Replies.Count(),
                Platform = BuildPlatformListing(mail)
            });

            var model = new PlatformTopicModel
            {
                Mails = mailListings,
                Platform = BuildPlatformListing(platform)
            };

            return View(model);
        } 

        [HttpPost]
        public IActionResult Search(int id, string searchQuery)
        {
            return RedirectToAction("Topic", new { id, searchQuery });
        }

        private PlatformListingModel BuildPlatformListing(Mail mail)
        {
            var platform = mail.Platform;

            return BuildPlatformListing(platform);
        }

        private PlatformListingModel BuildPlatformListing(Platform platform)
        {
            return new PlatformListingModel
            {
                Id = platform.Id,
                Name = platform.Title,
                Description = platform.Description,
                PlatformImageUrl = platform.ImageUrl
            };
        }
    }
}