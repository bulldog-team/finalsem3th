using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTO.User;
using API.Models;
using AutoMapper;
using BackEnd.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        public UserService(IConfiguration config, DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostEnvironment)
        {
            _httpContextAccessor = httpContextAccessor;
            _hostEnvironment = hostEnvironment;
            _context = context;
            _mapper = mapper;
            _config = config;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ResponseServiceModel<UserUpdateInfoDTO>> UserUpdateInfo(UserUpdateInfoDTO userRequest, int userRequestId)
        {
            var response = new ResponseServiceModel<UserUpdateInfoDTO>();
            var userId = GetUserId();
            var user = await _context.UserModels.Include(c => c.RoleDetailModels).ThenInclude(c => c.RoleModel).FirstOrDefaultAsync(c => c.UserId == userId);
            if (userId != userRequestId)
            {
                response.Success = false;
                response.Message = "Unauthorized!";
                return response;
            }
            if (await UserExists(userRequest.Username) || (await EmailExists(userRequest.Email)))
            {
                response.Message = "Username or Email exists!";
                response.Success = false;
                return response;
            }

            user.Username = userRequest.Username;
            user.Email = userRequest.Email;
            var userProfile = await _context.UserInfos.FirstOrDefaultAsync(c => c.UserId == userId);

            _mapper.Map(userRequest, userProfile);

            userProfile.ImgName = await SaveImage(userRequest.ImgFile);
            await _context.SaveChangesAsync();
            response.Data = userRequest;
            return response;
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imgFile)
        {
            var imgName = "default_img.png";
            if (imgFile != null)
            {
                imgName = new string(Path.GetFileNameWithoutExtension(imgFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                imgName = imgName + DateTime.Now.ToString("yyyymmssfff") + Path.GetExtension(imgFile.FileName);
                var imgPath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imgName);

                using (var fileStream = new FileStream(imgPath, FileMode.Create))
                {
                    await imgFile.CopyToAsync(fileStream);
                };
            }
            return imgName;

        }

        public async Task<bool> UserExists(string username)
        {
            var userId = GetUserId();
            return await _context.UserModels.AnyAsync(user => user.Username == username && user.UserId != userId);
        }

        public async Task<bool> EmailExists(string email)
        {
            var userId = GetUserId();
            return await _context.UserModels.AnyAsync(user => user.Email == email && user.UserId != userId);
        }

        public async Task<ResponseServiceModel<UserUpdateInfoDTO>> UserGetUserInfo(int id)
        {
            var response = new ResponseServiceModel<UserUpdateInfoDTO>();
            var userId = GetUserId();
            if (userId != id)
            {
                response.Success = false;
                response.Message = "Unauthorized!";
                return response;
            }
            var userProfile = await _context.UserInfos.Include(c => c.UserModel).FirstOrDefaultAsync(c => c.UserId == id);
            response.Data = new UserUpdateInfoDTO
            {
                Address = userProfile.Address,
                BranchId = userProfile.BranchId,
                Dob = userProfile.Dob,
                Email = userProfile.UserModel.Email,
                Username = userProfile.UserModel.Username,
                Phone = userProfile.Phone,
                Gender = userProfile.Gender,
                ImgName = userProfile.ImgName,
            };
            return response;

        }
    }
}