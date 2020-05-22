using DiscussionPlatform.Data;
using DiscussionPlatform.Data.Inerfaces;
using DiscussionPlatform.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task Create(Platform platform)
        {
            _context.Add(platform);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int platformId)
        {
            var platform = GetById(platformId);
            _context.Remove(platform);

            await _context.SaveChangesAsync();
        }

        public IEnumerable<ApplicationUser> GetActiveUsers(int id)
        {
            var mails = GetById(id).Mails;

            if(mails != null || !mails.Any())
            {
                var mailUsers = mails.Select(m => m.User);
                var replyUsers = mails.SelectMany(m => m.Replies).Select(r => r.User);

                return mailUsers.Union(replyUsers).Distinct();
            }

            return new List<ApplicationUser>();
        }

        public IEnumerable<Platform> GetAll()
        {
            return _context.Platforms.Include(platform => platform.Mails);
        }

        public Platform GetById(int id)
        {
            var platform = _context.Platforms.Where(p => p.Id == id)
                .Include(p => p.Mails).ThenInclude(m => m.User)
                .Include(p => p.Mails).ThenInclude(m => m.Replies).ThenInclude(r => r.User)
                .FirstOrDefault();

            return platform;
        }

        public bool HasRecentMail(int id)
        {
            const int hoursAgo = 12;
            var window = DateTime.Now.AddHours(-hoursAgo);

            return GetById(id).Mails.Any(mail => mail.DateOfCreation > window);
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
