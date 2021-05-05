using System.Threading.Tasks;
using API.DTO.User;
using API.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Get all users in list
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            var response = await _userService.GetUserList();
            return Ok(response);
        }

        // User get their info
        [Authorize]
        [HttpGet("info/{userRequestId}")]
        public async Task<IActionResult> UserGetUserInfo(int userRequestId)
        {
            var response = await _userService.UserGetUserInfo(userRequestId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        // User Update their info
        [Authorize]
        [HttpPatch("info/{userRequestId}")]
        public async Task<IActionResult> UserUpdateInfo([FromForm] UserUpdateInfoDTO user, int userRequestId)
        {
            var response = await _userService.UserUpdateInfo(user, userRequestId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(user);
        }

        // User Update their password
        [Authorize]
        [HttpPut("info/{userRequestId}")]
        public async Task<IActionResult> UpdatePassword(UserUpdatePasswordRequestDTO request, int userRequestId)
        {
            var response = await _userService.UpdatePassword(request, userRequestId);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
    }
}