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

        public PlatformController(IPlatform platformService)
        {
            _platformService = platformService;
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

        public IActionResult Topic(int id)
        {
            var platform = _platformService.GetById(id);
            var mails = platform.Mails;

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