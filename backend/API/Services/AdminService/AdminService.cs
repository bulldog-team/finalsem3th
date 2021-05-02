using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTO.Package;
using API.DTO.User;
using API.Models;
using AutoMapper;
using BackEnd.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

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
            var user = await _context.UserModels.FirstOrDefaultAsync(c => c.UserId == userId);
            if (curId == userId || user == null)
            {
                response.Success = false;
                return response;
            }

            _context.UserModels.Remove(user);
            await _context.SaveChangesAsync();
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

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("nguyenhuyhoang_test5@oisp.edu.vn");
            mail.To.Add(userRequest.Email);
            mail.Subject = "Create new user";
            mail.Body = $"Username: {userRequest.Username}<br> Password: {userRequest.Password} <br> Please change your password immediately";
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("nguyenhuyhoang_test5@oisp.edu.vn", "kien1@2024");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);

            return response;
        }

        public async Task<ResponseServiceModel<List<UpdatePriceRequestDTO>>> AdminUpdatePrice(List<UpdatePriceRequestDTO> request)
        {
            var response = new ResponseServiceModel<List<UpdatePriceRequestDTO>>();
            response.Data = new List<UpdatePriceRequestDTO>();
            foreach (var item in request)
            {
                var curType = await _context.DeliveryTypeModels.FirstOrDefaultAsync(c => c.TypeName == item.TypeName);
                var type = item.UnitPrice.GetType();
                if (type == typeof(int) && item.UnitPrice > 0 && curType != null)
                {
                    curType.UnitPrice = item.UnitPrice;
                    curType.TypeName = item.TypeName;
                    response.Data.Add(item);
                }
            }
            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<ResponseServiceModel<List<GetPriceListDTO>>> AdminGetPriceList()
        {
            var response = new ResponseServiceModel<List<GetPriceListDTO>>();
            var list = await _context.DeliveryTypeModels.Select(c => new GetPriceListDTO
            {
                TypeId = c.TypeId,
                TypeName = c.TypeName,
                UnitPrice = c.UnitPrice
            }).ToListAsync();
            response.Data = list;
            return response;

        }
    }
}