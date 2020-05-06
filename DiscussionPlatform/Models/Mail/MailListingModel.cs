using DiscussionPlatform.Models.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionPlatform.Models.Mail
{
    public class MailListingModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorId { get; set; }
        public string DateMailed { get; set; }

        public PlatformListingModel Platform { get; set; }

        public int RepliesCount { get; set; }
    }
}
