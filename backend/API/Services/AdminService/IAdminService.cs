using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO.User;
using API.Models;

namespace API.Services.AdminService
{
    public interface IAdminService
    {
        Task<ResponseServiceModel<AdminGetUserInfoDTO>> AdminGetUserInfo(int userId);

        Task<ResponseServiceModel<AdminGetUserInfoDTO>> AdminUpdateUserInfo(int userId);
    }
}