using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.DTO.User;
using API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        public UserService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;

        }
        public Task<ResponseServiceModel<UserUpdateInfoDTO>> UserUpdateInfo(UserUpdateInfoDTO user)
        {
            throw new System.NotImplementedException();
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imgFile)
        {
            var imgName = new string(Path.GetFileNameWithoutExtension(imgFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imgName = imgName + DateTime.Now.ToString("yyyymmssfff") + Path.GetExtension(imgFile.FileName);
            var imgPath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imgName);

            using (var fileStream = new FileStream(imgPath, FileMode.Create))
            {
                await imgFile.CopyToAsync(fileStream);
            };
            return imgName;

        }

    }
}