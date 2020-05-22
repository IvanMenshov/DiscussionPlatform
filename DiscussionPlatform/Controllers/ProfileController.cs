using System;
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

            //Подключение к хранилищу Azure
            var connectionString = _configuration.GetConnectionString("AzureStorageAccount");

            //Получить контейнер BLOB-объектов
            var container = _uploadService.GetBlobContainer(connectionString, "profile-images");

            //Разобрать заголовок ответа Расположение содержимого
            var contentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            //Получить имя файла
            var filename = contentDisposition.FileName.Trim('"');

            //Получить ссылку на блок Blob
            var blockBlob = container.GetBlockBlobReference(filename);

            //На этот блок BLOB Загрузить свой файл
            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());

            //Установите изображение профиля пользователя в URI
            await _userService.SetProfileImage(userId, blockBlob.Uri);

            //Перенаправить на страницу профиля пользователя
            return RedirectToAction("Detail", "Profile", new { id = userId });
        }
    }
}