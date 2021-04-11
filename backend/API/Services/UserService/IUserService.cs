using System.Threading.Tasks;
using API.DTO.User;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.UserService
{
    public interface IUserService
    {
        Task<ResponseServiceModel<UserUpdateInfoDTO>> UserUpdateInfo(UserUpdateInfoDTO user, int userRequestId);
    }
}