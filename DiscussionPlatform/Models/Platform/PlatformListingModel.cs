﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionPlatform.Models.Platform
{
    public class PlatformListingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PlatformImageUrl { get; set; }

        public int NumberOfMails { get; set; }
        public int NumberOfUsers { get; set; }
        public bool HasReсentMail { get; set; }
    }
}
