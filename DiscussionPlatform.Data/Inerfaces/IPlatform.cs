using DiscussionPlatform.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscussionPlatform.Data.Inerfaces
{
    public interface IPlatform
    {
        Platform GetById(int id);
        IEnumerable<Platform> GetAll();
        IEnumerable<ApplicationUser> GetAllActiveUsers();

        Task Create(Platform platform);
        Task Delete(int platformId);
        Task UpdatePlatformTitle(int platformId, string newTitle);
        Task UpdatePlatformDescription(int platformId, string newDescription);
    }
}
