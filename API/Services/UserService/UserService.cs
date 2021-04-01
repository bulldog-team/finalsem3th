using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO.User;
using API.Models;
using AutoMapper;
using BackEnd.Data;
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

        public async Task<ResponseServiceModel<IEnumerable<GetAllUserDTO>>> GetAllUsers()
        {
            var response = new ResponseServiceModel<IEnumerable<GetAllUserDTO>>();
            var allUsers = await _context.UserModels.ToListAsync();
            response.Data = _mapper.Map<IEnumerable<GetAllUserDTO>>(allUsers);
            return response;
        }
    }
}