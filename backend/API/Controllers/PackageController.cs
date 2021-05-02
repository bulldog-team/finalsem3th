using System.Threading.Tasks;
using API.DTO.Package;
using API.Services.PackageService;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
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

        [Authorize]
        [HttpGet("init")]
        public async Task<IActionResult> GetPackageList()
        {
            var response = await _packageService.GetPackageList();
            return Ok(response);
        }

        [Authorize]
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

        [Authorize]
        [HttpPost("info/{packageId}")]
        public async Task<IActionResult> UserUpdatePackageStatus(UserUpdatePackageStatus request, int packageId)
        {
            var response = await _packageService.UserUpdatePackageStatus(packageId, request);
            return Ok(response);
        }

        [Authorize]
        [HttpPut("info/{packageId}")]
        public async Task<IActionResult> UserUpdatePaymentPackage(int packageId)
        {
            var response = await _packageService.UserUpdatePaymentPackage(packageId);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("delivery-type")]
        public async Task<IActionResult> GetDeliveryType()
        {
            var response = await _packageService.GetDeliveryType();
            return Ok(response);
        }

        [HttpGet("search")]
        public IActionResult Searching([FromQuery] PackageResource request)
        {
            var response = _packageService.SearchPackage(request);
            return Ok(response);
        }
    }
}