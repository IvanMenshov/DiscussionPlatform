using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DiscussionPlatform.Data.Models;

namespace DiscussionPlatform.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<MailReply> MailReplies { get; set; }
    }
}
