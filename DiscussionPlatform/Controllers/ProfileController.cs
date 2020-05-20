﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DiscussionPlatform.Data.Inerfaces;
using DiscussionPlatform.Data.Models;
using DiscussionPlatform.Models;
using DiscussionPlatform.Models.ApplicationUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DiscussionPlatform.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IUpload _uploadService;
        private readonly IConfiguration _configuration;

        public ProfileController( UserManager<ApplicationUser> userManager,
            IApplicationUser userService, IUpload uploadService, IConfiguration configuration)
        {
            _userManager = userManager;
            _userService = userService;
            _uploadService = uploadService;
            _configuration = configuration;
        }

        public IActionResult Detail(string id)
        {
            var user = _userService.GetById(id);
            var userRoles = _userManager.GetRolesAsync(user).Result;

            var model = new ProfileModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserRating = user.Rating.ToString(),
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                PartnerSince = user.PartnerSince,
                IsAdmin = userRoles.Contains("Admin")
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            var userId = _userManager.GetUserId(User);

            //Connection to Azure Storage
            var connectionString = _configuration.GetConnectionString("AzureStorageAccount");

            //Get Blob Container
            var container = _uploadService.GetBlobContainer(connectionString);

            //Parse the Content Disposition response Header
            var contentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            //Grab the filename
            var filename = contentDisposition.FileName.Trim('"');

            //Get reference to a Block Blob
            var blockBlob = container.GetBlockBlobReference(filename);

            //On that block blob Upload our file
            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());

            //Set the User profile image to the URI
            await _userService.SetProfileImage(userId, blockBlob.Uri);

            //Redirect to the user profile page
            return RedirectToAction("Detail", "Profile", new { id = userId });
        }
    }
}