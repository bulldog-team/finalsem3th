using System.Linq;
using System.Threading.Tasks;
using API.DTO.User;
using API.Models;
using AutoMapper;
using BackEnd.Data;
using Microsoft.AspNetCore.Http;
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
                Phone = c.Phone
            }).Where(c => c.UserId == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                response.Success = false;
                response.Message = "Something wrongs!";
            }
            response.Data = user;
            return response;
        }
    }
}