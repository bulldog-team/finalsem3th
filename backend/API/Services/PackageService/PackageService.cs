using System.Threading.Tasks;
using API.DTO.Package;
using API.Models;
using AutoMapper;
using BackEnd.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Services.PackageService
{
    public class PackageService : IPackageService
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly PackageService _packageService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        public PackageService(IConfiguration config, DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostEnvironment)
        {
            _httpContextAccessor = httpContextAccessor;
            _hostEnvironment = hostEnvironment;
            _context = context;
            _mapper = mapper;
            _config = config;

        }
        public async Task<ResponseServiceModel<InitPackageDTO>> InitPackage(InitPackageDTO request)
        {
            var response = new ResponseServiceModel<InitPackageDTO>();
            var newPackage = _mapper.Map<PackageModel>(request);
            var deliveryType = await _context.DeliveryTypeModels.FirstOrDefaultAsync(c => c.TypeName == request.DeliveryType);

            if (deliveryType == null)
            {
                response.Message = "Something wrongs!";
                response.Success = false;
                return response;
            };
            var status = await _context.PackageStatusModels.FirstOrDefaultAsync(c => c.Status == "Picked up");
            status.Packages.Add(newPackage);
            newPackage.PackageStatus = status;

            newPackage.DeliveryType = deliveryType;
            deliveryType.Packages.Add(newPackage);

            await _context.PackageModels.AddAsync(newPackage);

            await _context.SaveChangesAsync();
            response.Data = request;
            return response;
        }
    }
}