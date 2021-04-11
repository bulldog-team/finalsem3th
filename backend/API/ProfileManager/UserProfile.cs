using API.DTO;
using API.DTO.User;
using API.Models;
using AutoMapper;

namespace API.ProfileManager
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // CreateMap<UserModel, GetUserDTO>().ForMember(itemInGetUserDTO => itemInGetUserDTO.RoleName, itemInUserModel => itemInUserModel.MapFrom(item => item.Role.RoleName));
            // CreateMap<UserRegisterDTO, UserModel>();
            CreateMap<UserModel, GetUserDTO>();
            CreateMap<UserUpdateInfoDTO, UserInfo>();
            CreateMap<UserInfoTemp, UserInfo>();
        }
    }
}