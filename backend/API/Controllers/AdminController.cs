using System.Threading.Tasks;
using API.Services.AdminService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("info/{userId}")]
        public async Task<IActionResult> AdminGetUserInfo(int userId)
        {
            var response = await _adminService.AdminGetUserInfo(userId);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

    }
}