using System.Threading.Tasks;
using API.DTO.User;
using API.Models;

namespace API.Services.UserService
{
    public interface IUserService
    {
        Task<ResponseServiceModel<UserRegisterDTO>> AdminCreateUser(UserRegisterDTO user);

        Task<ResponseServiceModel<UpdateUserDTO>> AdminUpdateUser(UpdateUserDTO user);

        Task<ResponseServiceModel<DeleteUserDTO>> AdminDeleteUser(DeleteUserDTO user);
    }
}