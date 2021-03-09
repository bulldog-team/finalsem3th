using API.DTO;
using API.Models;
using AutoMapper;

namespace API.ProfileManager
{
  public class UserProfile : Profile
  {
    public UserProfile()
    {
      CreateMap<UserModel, GetUserDTO>();
    }
  }
}