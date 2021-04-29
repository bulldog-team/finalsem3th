using API.DTO.Package;
using API.Models;
using AutoMapper;

namespace API.ProfileManager
{
    public class PackageProfile : Profile
    {
        public PackageProfile()
        {
            CreateMap<InitPackageDTO, PackageModel>().ForMember(c => c.DeliveryType, opt => opt.Ignore());
        }
    }
}