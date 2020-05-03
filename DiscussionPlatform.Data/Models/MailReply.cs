using DiscussionPlatform.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscussionPlatform.Data.Models
{
    public class MailReply
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DateOfCreation { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Mail Mail { get; set; }
    }
}
