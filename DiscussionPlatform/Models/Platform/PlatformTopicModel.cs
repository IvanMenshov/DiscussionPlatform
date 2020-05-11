using DiscussionPlatform.Models.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionPlatform.Models.Platform
{
    public class PlatformTopicModel
    {
        public PlatformListingModel Platform { get; set; }
        public IEnumerable<MailListingModel> Mails { get; set; }
        public string SearchQuery { get; set; }
    }
}
