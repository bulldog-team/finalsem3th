using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO.Package;
using API.Models;

namespace API.Services.PackageService
{
    public interface IPackageService
    {
        Task<ResponseServiceModel<InitPackageResponse>> InitPackage(InitPackageDTO request);
        Task<ResponseServiceModel<List<GetPackageListDTO>>> GetPackageList();

        Task<ResponseServiceModel<UserGetPackageDTO>> UserGetPackageInfo(int packageId);

        Task<ResponseServiceModel<List<DeliveryTypeModel>>> GetDeliveryType();

        Task<ResponseServiceModel<string>> UserUpdatePackageStatus(int packageId, UserUpdatePackageStatus txtStatus);

        Task<ResponseServiceModel<string>> UserUpdatePaymentPackage(int packageId);

        Task<ResponseServiceModel<string>> UserUpdatePackageInfo(int packageId, InitPackageDTO request);

        Task<ResponseServiceModel<string>> AdminDeletePackage(int packageId);

        Task<ResponseServiceModel<string>> UserUpdateCashPayment(int packageId);

        ResponseServiceModel<ICollection<SearchingPackageResponseDTO>> SearchPackage(PackageResource resource);
    }
}