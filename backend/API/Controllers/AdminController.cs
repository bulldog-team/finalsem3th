using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO.Package;
using API.DTO.User;
using API.Services.AdminService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // Admin Controller
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }


        // Admin get User Information
        [Authorize(Roles = "Admin")]
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

        // Admin aprrove User Information
        [Authorize(Roles = "Admin")]
        [HttpPatch("info/{userId}")]
        public async Task<IActionResult> AdminApproveUserInfo(int userId)
        {
            var response = await _adminService.AdminUpdateUserInfo(userId);
            return Ok(response);
        }

        // Admin create a new user
        [Authorize(Roles = "Admin")]
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

        // Admin delete user information
        [Authorize(Roles = "Admin")]
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

        // Admin update delivery type price list
        [Authorize(Roles = "Admin")]
        [HttpPatch("type")]
        public async Task<IActionResult> AdminUpdatePrice(List<UpdatePriceRequestDTO> request)
        {
            var response = await _adminService.AdminUpdatePrice(request);
            return Ok(response);
        }

        // Admin get all delivery type price list
        [Authorize(Roles = "Admin")]
        [HttpGet("type")]
        public async Task<IActionResult> AdminGetPrice()
        {
            var response = await _adminService.AdminGetPriceList();
            return Ok(response);
        }

    }
}