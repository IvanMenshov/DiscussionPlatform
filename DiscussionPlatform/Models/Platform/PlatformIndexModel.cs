using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionPlatform.Models.Platform
{
    public class PlatformIndexModel
    {
        public IEnumerable<PlatformListingModel> PlatformList { get; set; }
    }
}
