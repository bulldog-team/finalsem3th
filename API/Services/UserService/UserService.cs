using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO.User;
using API.Models;
using AutoMapper;
using BackEnd.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UserService(IConfiguration config, DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
        }

        public async Task<ResponseServiceModel<UserModel>> DeleteUser(int id)
        {
            var response = new ResponseServiceModel<UserModel>();
            var user = await _context.UserModels.FirstOrDefaultAsync(c => c.UserId == id);

            if (user == null)
            {
                response.Success = false;
                response.Message = "Something wrongs!";
                return response;
            }

            _context.UserModels.Remove(user);
            await _context.SaveChangesAsync();
            response.Data = user;
            return response;
        }

        public async Task<ResponseServiceModel<IEnumerable<GetAllUserDTO>>> GetAllUsers()
        {
            var response = new ResponseServiceModel<IEnumerable<GetAllUserDTO>>();
            var allUsers = await _context.UserModels.ToListAsync();
            response.Data = _mapper.Map<IEnumerable<GetAllUserDTO>>(allUsers);
            return response;
        }

        public async Task<ResponseServiceModel<UpdateUserDTO>> UpdateUser(UpdateUserDTO newUser, int userId)
        {
            var response = new ResponseServiceModel<UpdateUserDTO>();
            var user = await _context.UserModels.Include(c => c.RoleDetailModels).ThenInclude(c => c.RoleModel).FirstOrDefaultAsync(c => c.UserId == userId);

            if (user == null)
            {
                response.Success = false;
                response.Message = "Something wrongs!";
                return response;
            }

            foreach (var item in user.RoleDetailModels)
            {
                var foundRole = await _context.RoleModels.FirstOrDefaultAsync(c => c.RoleName.Equals(item.RoleModel.RoleName));

                var curRole = await _context.RoleDetailModels.FirstOrDefaultAsync(c => c.RoleId == foundRole.RoleId);

                _context.RoleDetailModels.Remove(curRole);
                await _context.SaveChangesAsync();

            }

            foreach (var item in newUser.Role)
            {
                var newRole = await _context.RoleModels.FirstOrDefaultAsync(c => c.RoleName.Equals(item));
                await _context.RoleDetailModels.AddAsync(new RoleDetailModel
                {
                    RoleId = newRole.RoleId,
                    UserId = userId,
                });
                await _context.SaveChangesAsync();
            }

            user.Email = newUser.Email;
            user.Password = new PasswordHasher<object>().HashPassword(null, newUser.Password);
            user.Username = newUser.Username;
            _context.UserModels.Update(user);
            await _context.SaveChangesAsync();
            response.Data = newUser;
            return response;

        }
    }
}