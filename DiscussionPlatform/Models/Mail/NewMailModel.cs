using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionPlatform.Models.Mail
{
    public class NewMailModel
    {
        public string PlatformName { get; set; }
        public int PlatformId { get; set; }
        public string AuthorName { get; set; }
        public string PlatformImageUrl { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
