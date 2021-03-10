using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.DTO;
using API.DTO.User;
using API.Models;
using AutoMapper;
using BackEnd.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly string _expDate;
        private readonly string _secret;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config, DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
            _secret = config.GetSection("JwtConfig").GetSection("secret").Value;
            _expDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
        }

        public bool ComparePassowrd(string hashedPassword, string curPassword)
        {
            var passwordVerificationResult = new PasswordHasher<object>().VerifyHashedPassword(null, hashedPassword, curPassword);
            switch (passwordVerificationResult)
            {
                case PasswordVerificationResult.Failed:
                    return false;

                case PasswordVerificationResult.Success:
                    return true;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string GenerateSecurityToken(GetUserDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.RoleName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<ResponseServiceModel<GetUserDTO>> Login(string username, string password)
        {
            var response = new ResponseServiceModel<GetUserDTO>();
            var user = await _context.UserModels.Include(c => c.Role).FirstOrDefaultAsync(x => x.Username.Equals(username));
            if (user == null)
            {
                response.Success = false;
                response.Message = "Invalid user credentials!";
            }
            else if (!ComparePassowrd(user.Password, password))
            {
                response.Success = false;
                response.Message = "Invalid user credentials!";
            }
            else
            {
                response.Data = _mapper.Map<GetUserDTO>(user);
            }
            return response;
        }

        public Task<ResponseServiceModel<UserRegisterDTO>> Register()
        {
            throw new NotImplementedException();
        }

        public bool UserExists(string username)
        {
            throw new NotImplementedException();
        }
    }
}