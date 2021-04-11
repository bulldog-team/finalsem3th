using System.Threading.Tasks;
using API.DTO.User;
using API.Models;

namespace API.Services.UserService
{
    public class UserService : IUserService
    {
        public Task<ResponseServiceModel<UserUpdateInfoDTO>> UserUpdateInfo(UserUpdateInfoDTO user)
        {
            throw new System.NotImplementedException();
        }
    }
}