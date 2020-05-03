using System;
using System.Collections.Generic;
using System.Text;

namespace DiscussionPlatform.Data.Models
{
    public class Platform
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string ImageUrl { get; set; }

        public virtual IEnumerable<Mail> Mails { get; set; }
    }
}
