using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO.User;
using API.Models;

namespace API.Services.UserService
{
    public interface IUserService
    {
        Task<ResponseServiceModel<IEnumerable<GetAllUserDTO>>> GetAllUsers();
    }
}