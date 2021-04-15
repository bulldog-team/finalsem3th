using System.Collections.Generic;
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

        Task<ResponseServiceModel<UserGetUserInfo>> UserGetUserInfo(int id);

        Task<ResponseServiceModel<ICollection<UserListDTO>>> GetUserList();

        Task<ResponseServiceModel<UserUpdatePasswordResponseDTO>> UpdatePassword(UserUpdatePasswordRequestDTO request, int userId);

        Task<bool> UserExists(string username);
        Task<bool> EmailExists(string email);


    }
}