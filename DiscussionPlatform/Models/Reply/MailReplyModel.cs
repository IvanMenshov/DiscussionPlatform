﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionPlatform.Models.Reply
{
    public class MailReplyModel
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorImageUrl { get; set; }
        public bool IsAuthorAdmin { get; set; }

        public DateTime DateOfCreation { get; set; }
        public string ReplyContent { get; set; }

        public int MailId { get; set; }
        public string MailTitle { get; set; }
        public string MailContent { get; set; }

        public string PlatformName { get; set; }
        public string PlatformImageUrl { get; set; }
        public int PlatformId { get; set; }
    }
}
