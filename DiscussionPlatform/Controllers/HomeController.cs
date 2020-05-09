using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DiscussionPlatform.Models;
using DiscussionPlatform.Models.Home;
using DiscussionPlatform.Data.Inerfaces;
using DiscussionPlatform.Models.Mail;
using DiscussionPlatform.Data.Models;
using DiscussionPlatform.Models.Platform;

namespace DiscussionPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMail _mailService;

        public HomeController(IMail mailService)
        {
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            var model = BuildHomeIndexModel();

            return View(model);
        }

        private HomeIndexModel BuildHomeIndexModel()
        {
            var latestMails = _mailService.GetLatestMails(10);

            var mails = latestMails.Select(mail => new MailListingModel
            {
                Id = mail.Id,
                Title = mail.Title,
                AuthorName = mail.User.UserName,
                AuthorId = mail.User.Id,
                AuthorRating = mail.User.Rating,
                DateMailed = mail.DateOfCreation.ToString(),
                RepliesCount = mail.Replies.Count(),
                Platform = GetPlatformListingForMail(mail)
            });

            return new HomeIndexModel
            {
                LatestMails = mails,
                SearchQuery = ""
            };
        }

        private PlatformListingModel GetPlatformListingForMail(Mail mail)
        {
            var platform = mail.Platform;

            return new PlatformListingModel
            {
                Id = platform.Id,
                Name = platform.Title,
                PlatformImageUrl = platform.ImageUrl
            };
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
