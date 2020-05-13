using System;
using System.Collections.Generic;

namespace DiscussionPlatform.Data.Models
{
    public class Mail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateOfCreation { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Platform Platform { get; set; }

        public virtual IEnumerable<MailReply> Replies { get; set; }
    }
}
