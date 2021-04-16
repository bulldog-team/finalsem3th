using System.Threading.Tasks;
using API.DTO.Package;
using API.Models;

namespace API.Services.PackageService
{
    public interface IPackageService
    {
        Task<ResponseServiceModel<InitPackageDTO>> InitPackage(InitPackageDTO request);
    }
}