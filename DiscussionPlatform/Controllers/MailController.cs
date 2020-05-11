using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscussionPlatform.Data.Inerfaces;
using DiscussionPlatform.Data.Models;
using DiscussionPlatform.Models;
using DiscussionPlatform.Models.Mail;
using DiscussionPlatform.Models.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionPlatform.Controllers
{
    public class MailController : Controller
    {
        private readonly IMail _mailService;
        private readonly IPlatform _platformService;

        private static UserManager<ApplicationUser> _userManager;

        public MailController(IMail mailService, IPlatform platformService, UserManager<ApplicationUser> userManager)
        {
            _mailService = mailService;
            _platformService = platformService;
            _userManager = userManager;
        }

        public IActionResult Index(int id)
        {
            var mail = _mailService.GetById(id);
            var replies = BuildMailReplies(mail.Replies);

            var model = new MailIndexModel
            {
                Id = mail.Id,
                Title = mail.Title,
                AuthorId = mail.User.Id,
                AuthorName = mail.User.UserName,
                AuthorImageUrl = mail.User.ProfileImageUrl,
                AuthorRating = mail.User.Rating,
                DateOfCreation = mail.DateOfCreation,
                MailContent = mail.Content,
                Replies = replies,
                PlatformId=mail.Platform.Id,
                PlatformName=mail.Platform.Title,
                IsAuthorAdmin = IsAuthorAdmin(mail.User)
            };

            return View(model);
        }

        private bool IsAuthorAdmin(ApplicationUser user)
        {
            return _userManager.GetRolesAsync(user).Result.Contains("Admin");
        }

        public IActionResult Create(int id)
        {
            var platform = _platformService.GetById(id);

            var model = new NewMailModel
            {
                PlatformName = platform.Title,
                PlatformId = platform.Id,
                PlatformImageUrl = platform.ImageUrl,
                AuthorName = User.Identity.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddMail(NewMailModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            var mail = BuildMail(model, user);

            await _mailService.Add(mail);

            return RedirectToAction("Index", "Mail", new { id = mail.Id });
        }

        private Mail BuildMail(NewMailModel model, ApplicationUser user)
        {
            var platform = _platformService.GetById(model.PlatformId);

            return new Mail
            {
                Title = model.Title,
                Content = model.Content,
                DateOfCreation = DateTime.Now,
                User = user,
                Platform = platform
            };
        }

        private IEnumerable<MailReplyModel> BuildMailReplies(IEnumerable<MailReply> replies)
        {
            return replies.Select(reply => new MailReplyModel
            {
                Id = reply.Id,
                AuthorName = reply.User.UserName,
                AuthorId = reply.User.Id,
                AuthorImageUrl = reply.User.ProfileImageUrl,
                AuthorRating = reply.User.Rating,
                DateOfCreation = reply.DateOfCreation,
                ReplyContent = reply.Content,
                IsAuthorAdmin = IsAuthorAdmin(reply.User)
            });
        }
    }
}