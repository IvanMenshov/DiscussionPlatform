using DiscussionPlatform.Models.Reply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionPlatform.Models.Mail
{
    public class MailIndexModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }
        public int AuthorRating { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string MailContent { get; set; }
        public bool IsAuthorAdmin { get; set; }

        public int PlatformId { get; set; }
        public string PlatformName { get; set; }

        public IEnumerable<MailReplyModel> Replies { get; set; }
    }
}
