using DiscussionPlatform.Data;
using DiscussionPlatform.Data.Inerfaces;
using DiscussionPlatform.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscussionPlatform.Service
{
    public class MailService : IMail
    {
        private readonly ApplicationDbContext _context;

        public MailService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Mail mail)
        {
            _context.Add(mail);
            await _context.SaveChangesAsync();
        }

        public async Task AddReply(MailReply reply)
        {
            _context.MailReplies.Add(reply);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditMailContent(int id, string newContent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Mail> GetAll()
        {
            return _context.Mails
                .Include(mail => mail.User)
                .Include(mail => mail.Replies).ThenInclude(reply => reply.User)
                .Include(mail => mail.Platform);
        }

        public Mail GetById(int id)
        {
            return _context.Mails.Where(mail => mail.Id == id)
                .Include(mail => mail.Platform)
                .Include(mail => mail.User)
                .Include(mail => mail.Replies).ThenInclude(reply=>reply.User)
                .FirstOrDefault();
        }

        public IEnumerable<Mail> GetFilteredMails(Platform platform, string searchQuery)
        {
            return string.IsNullOrEmpty(searchQuery)
                ? platform.Mails : platform.Mails.Where(mail => mail.Title.Contains(searchQuery)
                || mail.Content.Contains(searchQuery));
        }

        public IEnumerable<Mail> GetFilteredMails(string searchQuery)
        {
            var normalized = searchQuery.ToLower();

            return GetAll().Where(mail => mail.Title.ToLower().Contains(normalized)
            || mail.Content.ToLower().Contains(normalized));
        }

        public IEnumerable<Mail> GetLatestMails(int n)
        {
            return GetAll().OrderByDescending(mail => mail.DateOfCreation).Take(n);
        }

        public IEnumerable<Mail> GetMailByPlatform(int id)
        {
            return _context.Platforms
                .Where(platform => platform.Id == id).First().Mails;
        }
    }
}
