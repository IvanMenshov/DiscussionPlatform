using DiscussionPlatform.Data;
using DiscussionPlatform.Data.Models;
using DiscussionPlatform.Service;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscussionPlatform.Test
{
    [TestFixture]
    public class Mail_Service_Should
    {
        [TestCase("coffee", 3)]
        [TestCase("tea", 1)]
        [TestCase("water", 0)] 
        public void Return_Filtered_Results_Corresponding_To_Query(string query, int expected)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            //Arrange
            using (var ctx = new ApplicationDbContext(options))
            {
                ctx.Platforms.Add(new Platform
                {
                    Id = 19
                });

                ctx.Mails.Add(new Mail
                {
                    Platform = ctx.Platforms.Find(19),
                    Id = 23523,
                    Title = "First mail",
                    Content = "Coffee"
                });

                ctx.Mails.Add(new Mail
                {
                    Platform = ctx.Platforms.Find(19),
                    Id = 1564,
                    Title = "Coffee",
                    Content = "Some content"
                });

                ctx.Mails.Add(new Mail
                {
                    Platform = ctx.Platforms.Find(19),
                    Id = 354,
                    Title = "Tea",
                    Content = "Coffee"
                });

                ctx.SaveChanges();
            }

            //Act
            using (var ctx = new ApplicationDbContext(options))
            {
                var mailService = new MailService(ctx);
                var result = mailService.GetFilteredMails(query);
                var mailCount = result.Count();

                //Assert
                Assert.AreEqual(expected, mailCount);
            }
        }

        [Test]
        public void Get_Mail_By_Id_Return_Correct_Mail()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_Mail_By_Id_Db").Options;

            using (var ctx = new ApplicationDbContext(options))
            {
                ctx.Mails.Add(new Mail { Id = 1986, Title = "first" });
                ctx.Mails.Add(new Mail { Id = 223, Title = "second" });
                ctx.Mails.Add(new Mail { Id = 12, Title = "third" });
                ctx.SaveChanges();
            }

            using(var ctx = new ApplicationDbContext(options))
            {
                var mailService = new MailService(ctx);
                var result = mailService.GetById(223);

                Assert.AreEqual(result.Title, "second");
            }
        }

        [Test]
        public void Get_All_Mails_Return_All_Mails()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_All_Mails_Db").Options;

            using (var ctx = new ApplicationDbContext(options))
            {
                ctx.Mails.Add(new Mail { Id = 1986, Title = "first" });
                ctx.Mails.Add(new Mail { Id = 223, Title = "second" });
                ctx.Mails.Add(new Mail { Id = 12, Title = "third" });
                ctx.SaveChanges();
            }

            using (var ctx = new ApplicationDbContext(options))
            {
                var mailService = new MailService(ctx);
                var result = mailService.GetAll();

                Assert.AreEqual(3, result.Count());
            }
        }
    }
}
