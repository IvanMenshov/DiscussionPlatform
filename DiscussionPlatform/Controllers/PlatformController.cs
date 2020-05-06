using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscussionPlatform.Data.Inerfaces;
using DiscussionPlatform.Data.Models;
using DiscussionPlatform.Models.Platform;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionPlatform.Controllers
{
    public class PlatformController : Controller
    {
        private readonly IPlatform _platformService;

        public PlatformController(IPlatform platformService)
        {
            _platformService = platformService; 
        }

        public IActionResult Index()
        {
            var platforms = _platformService.GetAll()
                 .Select(platform => new PlatformListingModel
                 {
                     Id = platform.Id,
                     Name = platform.Title,
                     Description = platform.Description
                 });

            var model = new PlatformIndexModel
            {
                PlatformList = platforms
            };

            return View(model);
        }

        //public IActionResult Topic(int id)
        //{
        //    var platform = _platformService.GetById(id);
        //}
    }
}