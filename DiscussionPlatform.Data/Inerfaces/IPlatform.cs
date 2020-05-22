using DiscussionPlatform.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscussionPlatform.Data.Inerfaces
{
    public interface IPlatform
    {
        Platform GetById(int id);
        IEnumerable<Platform> GetAll();

        Task Create(Platform platform);
        Task Delete(int platformId);
        Task UpdatePlatformTitle(int platformId, string newTitle);
        Task UpdatePlatformDescription(int platformId, string newDescription);

        IEnumerable<ApplicationUser> GetActiveUsers(int id);
        bool HasRecentMail(int id);
    }
}
