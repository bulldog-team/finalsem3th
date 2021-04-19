using System.Threading.Tasks;
using API.DTO.Package;
using API.Services.PackageService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;
        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpPost("init")]
        public async Task<IActionResult> InitPackage(InitPackageDTO request)
        {
            var response = await _packageService.InitPackage(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("init")]
        public async Task<IActionResult> GetPackageList()
        {
            var response = await _packageService.GetPackageList();
            return Ok(response);
        }

        [HttpGet("info/{packageId}")]
        public async Task<IActionResult> UserGetPackageInfo(int packageId)
        {
            var response = await _packageService.UserGetPackageInfo(packageId);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [HttpGet("delivery-type")]
        public async Task<IActionResult> GetDeliveryType()
        {
            var response = await _packageService.GetDeliveryType();
            return Ok(response);
        }
    }
}