using DiscussionPlatform.Data;
using DiscussionPlatform.Service;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace DiscussionPlatform.Test
{
    public class Class1
    {
        //[TestFixture]
        //[Category("Services")]
        //public class ForumServiceTests
        //{
        //    [Test]
        //    public void Filtered_Posts_Returns_Correct_Result_Count()
        //    {
        //        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //            .UseInMemoryDatabase(databaseName: "Search_Database").Options;

        //        using (var ctx = new ApplicationDbContext(options))
        //        {
        //            ctx.Platforms.Add(new Data.Models.Platform()
        //            {
        //                Id = 19
        //            });

        //            ctx.Mails.Add(new Data.Models.Mail
        //            {
        //                Platform = ctx.Platforms.Find(19),
        //                Id = 21341,
        //                Title = "Functional programming",
        //                Content = "Does anyone have experience deploying Haskell to production?"
        //            });

        //            ctx.Mails.Add(new Data.Models.Mail
        //            {
        //                Platform = ctx.Platforms.Find(19),
        //                Id = -324,
        //                Title = "Haskell Tail Recursion",
        //                Content = "Haskell Haskell"
        //            });

        //            ctx.SaveChanges();
        //        }

        //        using (var ctx = new ApplicationDbContext(options))
        //        {
        //            var mailService = new MailService(ctx);
        //            var platformService = new PlatformService(ctx, mailService);
        //            var mailCount = platformService.GetFilteredMails(19, "Haskell").Count();
        //            Assert.AreEqual(2, mailCount);
        //        }
        //    }
        //}
    }
}
