using System;
using System.Collections.Generic;
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

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach (var item in user.Role)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }
            var subject = claims.ToArray();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(subject),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<ResponseServiceModel<GetUserDTO>> Login(UserLoginDTO request)
        {
            var response = new ResponseServiceModel<GetUserDTO>();
            var user = await _context.UserModels.Include(c => c.RoleDetailModels).ThenInclude(c => c.RoleModel).FirstOrDefaultAsync(x => x.Username.Equals(request.Username));
            if (user == null)
            {
                response.Success = false;
                response.Message = "Invalid user credentials!";
            }
            else if (!ComparePassowrd(user.Password, request.Password))
            {
                response.Success = false;
                response.Message = "Invalid user credentials!";
            }
            else
            {
                var userDTO = _mapper.Map<GetUserDTO>(user);
                var role = new List<string>();
                foreach (var item in user.RoleDetailModels)
                {
                    role.Add(item.RoleModel.RoleName);
                }
                userDTO.Role = role.ToArray();
                userDTO.Token = GenerateSecurityToken(userDTO);
                response.Data = userDTO;
            }
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.UserModels.AnyAsync(c => c.Username == username);
        }

        public async Task<ResponseServiceModel<GetUserDTO>> Register(UserRegisterDTO request)
        {
            var userExists = await UserExists(request.Username);
            var response = new ResponseServiceModel<GetUserDTO>();
            if (!userExists)
            {
                var mappedToUserModel = _mapper.Map<UserModel>(request);
                mappedToUserModel.Password = new PasswordHasher<object>().HashPassword(null, request.Password);

                _context.UserModels.Add(mappedToUserModel);
                await _context.SaveChangesAsync();

                var savedUser = await _context.UserModels.FirstOrDefaultAsync(c => c.Username == request.Username);
                var mapSaveUserToGetUserDTO = _mapper.Map<GetUserDTO>(savedUser);
                mapSaveUserToGetUserDTO.Token = GenerateSecurityToken(mapSaveUserToGetUserDTO);

                response.Data = mapSaveUserToGetUserDTO;
                return response;
            }
            response.Success = false;
            response.Message = "User already exists!";
            return response;
        }

    }
}