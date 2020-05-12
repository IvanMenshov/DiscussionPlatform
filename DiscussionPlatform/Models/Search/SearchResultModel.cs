using DiscussionPlatform.Models.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionPlatform.Models.Search
{
    public class SearchResultModel
    {
        public IEnumerable<MailListingModel> Mails { get; set; }
        public string SearchQuery { get; set; }
        public bool EmptySearchResult { get; set; }
    }
}
