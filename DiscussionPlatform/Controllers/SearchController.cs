using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscussionPlatform.Data.Inerfaces;
using DiscussionPlatform.Data.Models;
using DiscussionPlatform.Models.Mail;
using DiscussionPlatform.Models.Platform;
using DiscussionPlatform.Models.Search;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionPlatform.Controllers
{
    public class SearchController : Controller
    {
        private readonly IMail _mailService;

        public SearchController(IMail mailService)
        {
            _mailService = mailService;
        }

        public IActionResult Results(string searchQuery)
        {
            var mails = _mailService.GetFilteredMails(searchQuery);
            var areNoResults = (!string.IsNullOrEmpty(searchQuery) && !mails.Any());

            var mailListings = mails.Select(mail => new MailListingModel
            {
                Id = mail.Id,
                AuthorId = mail.User.Id,
                AuthorName = mail.User.UserName,
                AuthorRating = mail.User.Rating,
                Title = mail.Title,
                DateMailed = mail.DateOfCreation.ToString(),
                RepliesCount = mail.Replies.Count(),
                Platform = BuildPltformListing(mail)
            });

            var model = new SearchResultModel
            {
                Mails = mailListings,
                SearchQuery = searchQuery,
                EmptySearchResult = areNoResults
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Search(string searchQuery)
        {
            return RedirectToAction("Results", new { searchQuery });
        }

        private PlatformListingModel BuildPltformListing(Mail mail)
        {
            var platform = mail.Platform;

            return new PlatformListingModel
            {
                Id = platform.Id,
                PlatformImageUrl = platform.ImageUrl,
                Name = platform.Title,
                Description = platform.Description
            };
        }
    }
}