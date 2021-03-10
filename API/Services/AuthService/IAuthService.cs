using System.Threading.Tasks;
using API.DTO;
using API.DTO.User;
using API.Models;

namespace API.Services.AuthService
{
    public interface IAuthService
    {
        string GenerateSecurityToken(GetUserDTO user);
        Task<ResponseServiceModel<GetUserDTO>> Login(string username, string password);
        Task<ResponseServiceModel<UserRegisterDTO>> Register();
        bool UserExists(string username);
        bool ComparePassowrd(string hashedPassword, string curPassword);
    }
}