using DiscussionPlatform.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscussionPlatform.Data.Inerfaces
{
    public interface IApplicationUser
    {
        ApplicationUser GetById(string id);
        IEnumerable<ApplicationUser> GetAll();

        Task SetProfileImage(string id, Uri uri);
        Task IncementRating(string id, Type type);
    }
}
