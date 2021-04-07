using System.Threading.Tasks;
using API.DTO;
using API.DTO.User;
using API.Models;

namespace API.Services.AuthService
{
    public interface IAuthService
    {
        Task<ResponseServiceModel<GetUserDTO>> Login(UserLoginDTO user);
        Task<ResponseServiceModel<GetUserDTO>> Register(UserRegisterDTO user);
        string GenerateSecurityToken(GetUserDTO user);
        Task<bool> UserExists(string username);
        bool ComparePassowrd(string hashedPassword, string curPassword);
    }
}