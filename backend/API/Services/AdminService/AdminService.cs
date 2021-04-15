using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTO.User;
using API.Models;
using AutoMapper;
using BackEnd.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        public AdminService(IConfiguration config, DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
            _config = config;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ResponseServiceModel<AdminGetUserInfoDTO>> AdminUpdateUserInfo(int userId)
        {
            var response = new ResponseServiceModel<AdminGetUserInfoDTO>();
            var user = await _context.UserInfos.Include(c => c.UserModel).FirstOrDefaultAsync(c => c.UserId == userId);
            user.IsAdminAccept = !user.IsAdminAccept;
            await _context.SaveChangesAsync();

            if (user != null && user.Branch != null)
            {
                response.Data = new AdminGetUserInfoDTO
                {
                    Address = user.Address,
                    BranchName = user.Branch.BranchName,
                    Dob = user.Dob,
                    Email = user.UserModel.Email,
                    Gender = user.Gender,
                    IsAdminAccept = user.IsAdminAccept,
                    ImgName = user.ImgName,
                    Phone = user.Phone
                };
                return response;
            }
            response.Success = false;
            return response;

        }

        public async Task<ResponseServiceModel<AdminGetUserInfoDTO>> AdminGetUserInfo(int userId)
        {
            var response = new ResponseServiceModel<AdminGetUserInfoDTO>();
            var user = await _context.UserInfos.Include(c => c.UserModel).Select(c => new AdminGetUserInfoDTO
            {
                Address = c.Address,
                BranchId = c.BranchId,
                ImgName = c.ImgName,
                IsAdminAccept = c.IsAdminAccept,
                Dob = c.Dob,
                Email = c.UserModel.Email,
                Username = c.UserModel.Username,
                UserId = c.UserId,
                Gender = c.Gender,
                Phone = c.Phone,
                BranchName = c.Branch.BranchName

            }).Where(c => c.UserId == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                response.Success = false;
                response.Message = "Something wrongs!";
            }
            response.Data = user;
            return response;
        }

        public async Task<ResponseServiceModel<DeleteUserDTO>> AdminDeleteUserInfo(int userId)
        {
            var response = new ResponseServiceModel<DeleteUserDTO>();
            var curId = GetUserId();
            if (curId == userId)
            {
                response.Success = false;
                return response;
            }

            var user = await _context.UserModels.FirstOrDefaultAsync(c => c.UserId == userId);
            _context.UserModels.Remove(user);
            response.Data = _mapper.Map<DeleteUserDTO>(user);
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.UserModels.AnyAsync(c => c.Username == username);
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _context.UserModels.AnyAsync(c => c.Email == email);
        }

        public async Task<ResponseServiceModel<AdminCreateUserResponse>> AdminCreateuser(AdminCreateUser userRequest)
        {
            var response = new ResponseServiceModel<AdminCreateUserResponse>();
            if (await EmailExists(userRequest.Email) || (await UserExists(userRequest.Username)) || (userRequest.Password != userRequest.ConfirmPassword) || (userRequest.Password.Length < 6))
            {
                response.Success = false;
                response.Message = "Something wrongs!";
                return response;
            }
            var newUser = new UserModel
            {
                Email = userRequest.Email,
                Password = new PasswordHasher<object>().HashPassword(null, userRequest.Password),
                Username = userRequest.Username
            };

            var userRole = await _context.RoleModels.FirstOrDefaultAsync(c => c.RoleName == "User");

            newUser.RoleDetailModels = new List<RoleDetailModel> {
                new RoleDetailModel{
                    RoleModel = userRole,
                    User = newUser
                }
            };

            var defaultBranch = await _context.BranchModels.FirstOrDefaultAsync(c => c.BranchName == "HCM");

            newUser.UserInfo = new UserInfo
            {
                Dob = DateTime.Now,
                Branch = defaultBranch,
            };

            await _context.UserModels.AddAsync(newUser);
            await _context.SaveChangesAsync();

            var repsonseUser = new AdminCreateUserResponse
            {
                Email = newUser.Email,
                UserId = newUser.Id,
                Username = newUser.Username
            };
            response.Data = repsonseUser;
            return response;
        }
    }
}