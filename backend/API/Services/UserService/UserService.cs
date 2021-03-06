using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTO.User;
using API.Models;
using API.Services.AuthService;
using AutoMapper;
using BackEnd.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly IAuthService _authService;
        public UserService(IConfiguration config, DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostEnvironment, IAuthService authService)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
            _hostEnvironment = hostEnvironment;
            _context = context;
            _mapper = mapper;
            _config = config;
        }

        // Get UserId in JWT
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        // User Update their info
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
        // Save Image 
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

        // Check username exists
        public async Task<bool> UserExists(string username)
        {
            var userId = GetUserId();
            return await _context.UserModels.AnyAsync(user => user.Username == username && user.UserId != userId);
        }

        // Check email exists
        public async Task<bool> EmailExists(string email)
        {
            var userId = GetUserId();
            return await _context.UserModels.AnyAsync(user => user.Email == email && user.UserId != userId);
        }

        // User get their info
        public async Task<ResponseServiceModel<UserGetUserInfo>> UserGetUserInfo(int id)
        {
            var response = new ResponseServiceModel<UserGetUserInfo>();
            var userId = GetUserId();
            if (userId != id)
            {
                response.Success = false;
                response.Message = "Unauthorized!";
                return response;
            }
            var userProfile = await _context.UserInfos.Include(c => c.UserModel).FirstOrDefaultAsync(c => c.UserId == id);
            response.Data = new UserGetUserInfo
            {
                Address = userProfile.Address,
                BranchId = userProfile.BranchId,
                Dob = userProfile.Dob,
                Email = userProfile.UserModel.Email,
                Username = userProfile.UserModel.Username,
                Phone = userProfile.Phone,
                Gender = userProfile.Gender,
                ImgName = userProfile.ImgName,
                IsAdminAccept = userProfile.IsAdminAccept
            };
            return response;
        }

        // Get all user
        public async Task<ResponseServiceModel<ICollection<UserListDTO>>> GetUserList()
        {
            var response = new ResponseServiceModel<ICollection<UserListDTO>>();
            var user = await _context.UserInfos.Select(c => new UserListDTO
            {
                UserId = c.UserId,
                Branch = c.Branch.BranchName,
                Email = c.UserModel.Email,
                IsAdminAccept = c.IsAdminAccept,
                Username = c.UserModel.Username
            }).ToListAsync<UserListDTO>();
            response.Data = user;
            return response;
        }

        // User update their password
        public async Task<ResponseServiceModel<UserUpdatePasswordResponseDTO>> UpdatePassword(UserUpdatePasswordRequestDTO request, int userId)
        {
            var response = new ResponseServiceModel<UserUpdatePasswordResponseDTO>();
            var curUserId = GetUserId();
            var user = await _context.UserModels.FirstOrDefaultAsync(c => c.UserId == curUserId);

            if (curUserId != userId || request.Password != request.ConfirmPassword || !_authService.ComparePassowrd(user.Password, request.CurrentPassword))
            {
                response.Success = false;
                response.Message = "Something wrongs!";
                return response;
            }

            user.Password = new PasswordHasher<object>().HashPassword(null, request.Password);
            await _context.SaveChangesAsync();
            response.Data = new UserUpdatePasswordResponseDTO
            {
                UserId = curUserId,
                Username = user.Username,
            };

            return response;
        }
    }
}