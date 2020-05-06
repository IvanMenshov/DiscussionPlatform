using DiscussionPlatform.Data;
using DiscussionPlatform.Data.Inerfaces;
using DiscussionPlatform.Data.Models;
using DiscussionPlatform.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscussionPlatform.Service
{
    public class PlatformService : IPlatform
    {
        private readonly ApplicationDbContext _context;

        public PlatformService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task Create(Platform platform)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int platformId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Platform> GetAll()
        {
            return _context.Platforms.Include(platform => platform.Mails);
        }

        public IEnumerable<ApplicationUser> GetAllActiveUsers()
        {
            throw new NotImplementedException();
        }

        public Platform GetById(int id)
        {
            var platform = _context.Platforms.Where(p => p.Id == id)
                .Include(p => p.Mails).ThenInclude(m => m.User)
                .Include(p => p.Mails).ThenInclude(m => m.Replies).ThenInclude(r => r.User)
                .FirstOrDefault();

            return platform;
        }

        public Task UpdatePlatformDescription(int platformId, string newDescription)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePlatformTitle(int platformId, string newTitle)
        {
            throw new NotImplementedException();
        }
    }
}
