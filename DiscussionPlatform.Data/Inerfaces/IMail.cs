using DiscussionPlatform.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscussionPlatform.Data.Inerfaces
{
    public interface IMail
    {
        Mail GetById(int id);
        IEnumerable<Mail> GetAll();
        IEnumerable<Mail> GetFilteredMails(string searchQuery);
        IEnumerable<Mail> GetMailByPlatform(int id);
        IEnumerable<Mail> GetLatestMails(int n);

        Task Add(Mail mail);
        Task Delete(int id);
        Task EditMailContent(int id, string newContent);
    }
}
