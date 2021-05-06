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

        // Create a new package
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

        // Get all packages
        [Authorize]
        [HttpGet("init")]
        public async Task<IActionResult> GetPackageList()
        {
            var response = await _packageService.GetPackageList();
            return Ok(response);
        }

        // Get package information base on packageId
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

        // Update packge status
        [Authorize]
        [HttpPost("info/{packageId}")]
        public async Task<IActionResult> UserUpdatePackageStatus(UserUpdatePackageStatus request, int packageId)
        {
            var response = await _packageService.UserUpdatePackageStatus(packageId, request);
            return Ok(response);
        }

        [Authorize]
        [HttpPut("payment/{packageId}")]
        public async Task<IActionResult> UserUpdateCashPayment(int packageId)
        {
            var response = await _packageService.UserUpdateCashPayment(packageId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // Update payment status
        [Authorize]
        [HttpPut("info/{packageId}")]
        public async Task<IActionResult> UserUpdatePaymentPackage(int packageId)
        {
            var response = await _packageService.UserUpdatePaymentPackage(packageId);
            return Ok(response);
        }

        [Authorize]
        [HttpPatch("info/{packageId}")]
        public async Task<IActionResult> UserUpdatePackageInfo(int packageId, InitPackageDTO request)
        {
            var response = await _packageService.UserUpdatePackageInfo(packageId, request);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("info/{packageId}")]
        public async Task<IActionResult> AdminDeletePackage(int packageId)
        {
            var response = await _packageService.AdminDeletePackage(packageId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        // Get all delivery type
        [Authorize]
        [HttpGet("delivery-type")]
        public async Task<IActionResult> GetDeliveryType()
        {
            var response = await _packageService.GetDeliveryType();
            return Ok(response);
        }

        // Search packages
        [HttpGet("search")]
        public IActionResult Searching([FromQuery] PackageResource request)
        {
            var response = _packageService.SearchPackage(request);
            return Ok(response);
        }
    }
}