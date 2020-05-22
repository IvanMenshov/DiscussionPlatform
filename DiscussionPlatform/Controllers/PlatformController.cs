using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DiscussionPlatform.Data.Inerfaces;
using DiscussionPlatform.Data.Models;
using DiscussionPlatform.Models.Mail;
using DiscussionPlatform.Models.Platform;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;

namespace DiscussionPlatform.Controllers
{
    public class PlatformController : Controller
    {
        private readonly IPlatform _platformService;
        private readonly IMail _mailService;
        private readonly IUpload _uploadService;
        private readonly IConfiguration _configuration;

        public PlatformController(IPlatform platformService, IMail mailService, IUpload uploadService, IConfiguration configuration)
        {
            _platformService = platformService;
            _mailService = mailService;
            _uploadService = uploadService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var platforms = _platformService.GetAll()
                 .Select(platform => new PlatformListingModel
                 {
                     Id = platform.Id,
                     Name = platform.Title,
                     Description = platform.Description,
                     NumberOfMails = platform.Mails?.Count() ?? 0,
                     NumberOfUsers = _platformService.GetActiveUsers(platform.Id).Count(),
                     PlatformImageUrl = platform.ImageUrl,
                     HasReсentMail = _platformService.HasRecentMail(platform.Id)
                 });

            var model = new PlatformIndexModel
            {
                PlatformList = platforms.OrderBy(p=>p.Name)
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

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var model = new AddPlatformModel();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPlatform(AddPlatformModel model)
        {
            var imageUri = "images/users/default.png";

            if(model.ImageUpload != null)
            {
                var blockBlob = UploadPlatformImage(model.ImageUpload);
                imageUri = blockBlob.Uri.AbsoluteUri;
            }

            var platform = new Platform
            {
                Title = model.Title,
                Description = model.Description,
                DateOfCreation = DateTime.Now,
                ImageUrl = imageUri
            };

            await _platformService.Create(platform);
            return RedirectToAction("Index", "Platform");
        }

        private CloudBlockBlob UploadPlatformImage(IFormFile file)
        {
            var connectionString = _configuration.GetConnectionString("AzureStorageAccount");

            var container = _uploadService.GetBlobContainer(connectionString, "platform-images");

            var contentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            var filename = contentDisposition.FileName.Trim('"');

            var blockBlob = container.GetBlockBlobReference(filename);

            blockBlob.UploadFromStreamAsync(file.OpenReadStream()).Wait();

            return blockBlob;
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