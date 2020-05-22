using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscussionPlatform.Data.Inerfaces;
using DiscussionPlatform.Data.Models;
using DiscussionPlatform.Models.Reply;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionPlatform.Controllers
{
    [Authorize]
    public class ReplyController : Controller
    {
        private readonly IMail _mailService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;

        public ReplyController(IMail mailService, UserManager<ApplicationUser> userManager, IApplicationUser userService)
        {
            _mailService = mailService;
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<IActionResult> Create(int id)
        {
            var mail = _mailService.GetById(id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new MailReplyModel
            {
                MailContent = mail.Content,
                MailTitle = mail.Title,
                MailId = mail.Id,

                AuthorId = user.Id,
                AuthorName = User.Identity.Name,
                AuthorImageUrl = user.ProfileImageUrl,
                AuthorRating = user.Rating,
                IsAuthorAdmin = User.IsInRole("Admin"),

                PlatformId = mail.Platform.Id,
                PlatformName = mail.Platform.Title,
                PlatformImageUrl = mail.Platform.ImageUrl,

                DateOfCreation = DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReply(MailReplyModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            var reply = BuildReply(model, user);

            await _mailService.AddReply(reply);
            await _userService.UpdateUserRating(userId, typeof(MailReply));

            return RedirectToAction("Index", "Mail", new { id = model.MailId });
        }

        private MailReply BuildReply(MailReplyModel model, ApplicationUser user)
        {
            var mail = _mailService.GetById(model.MailId);

            return new MailReply
            {
                Mail = mail,
                Content = model.ReplyContent,
                DateOfCreation = DateTime.Now,
                User = user
            };
        }
    }
}