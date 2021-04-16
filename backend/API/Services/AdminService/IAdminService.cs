using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO.Package;
using API.DTO.User;
using API.Models;

namespace API.Services.AdminService
{
    public interface IAdminService
    {
        Task<ResponseServiceModel<AdminGetUserInfoDTO>> AdminGetUserInfo(int userId);

        Task<ResponseServiceModel<AdminGetUserInfoDTO>> AdminUpdateUserInfo(int userId);

        Task<ResponseServiceModel<DeleteUserDTO>> AdminDeleteUserInfo(int userId);

        Task<ResponseServiceModel<AdminCreateUserResponse>> AdminCreateuser(AdminCreateUser userRequest);
        Task<bool> UserExists(string username);
        Task<bool> EmailExists(string email);

        Task<ResponseServiceModel<List<UpdatePriceRequestDTO>>> AdminUpdatePrice(List<UpdatePriceRequestDTO> request);

        Task<ResponseServiceModel<List<GetPriceListDTO>>> AdminGetPriceList();


    }
}