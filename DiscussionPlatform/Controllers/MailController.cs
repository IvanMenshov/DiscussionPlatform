using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscussionPlatform.Data.Inerfaces;
using DiscussionPlatform.Data.Models;
using DiscussionPlatform.Models.Mail;
using DiscussionPlatform.Models.Reply;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionPlatform.Controllers
{
    public class MailController : Controller
    {
        private readonly IMail _mailService;

        public MailController(IMail mailService)
        {
            _mailService = mailService;
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
                Replies = replies
            };

            return View(model);
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
                ReplyContent = reply.Content
            });
        }
    }
}