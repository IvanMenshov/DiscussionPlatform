﻿using DiscussionPlatform.Data;
using DiscussionPlatform.Data.Inerfaces;
using DiscussionPlatform.Data.Models;
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

        public Task Add(Mail mail)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Mail GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Mail> GetFilteredMails(string searchQuery)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Mail> GetMailByPlatform(int id)
        {
            return _context.Platforms
                .Where(platform => platform.Id == id).First().Mails;
        }
    }
}
