using System.Threading.Tasks;
using API.DTO.User;
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

        [HttpPatch("info/{userId}")]
        public async Task<IActionResult> AdminApproveUserInfo(int userId)
        {
            var response = await _adminService.AdminUpdateUserInfo(userId);
            return Ok(response);
        }

        [HttpPost("info")]
        public async Task<IActionResult> AdminCreateuser(AdminCreateUser userRequest)
        {
            var response = await _adminService.AdminCreateuser(userRequest);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpDelete("info/{userId}")]
        public async Task<IActionResult> AdminDeleteUser(int userId)
        {
            var response = await _adminService.AdminDeleteUserInfo(userId);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

    }
}