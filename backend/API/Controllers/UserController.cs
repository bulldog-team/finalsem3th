using System.Threading.Tasks;
using API.DTO.User;
using API.Services.UserService;
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

        [HttpGet("{userRequestId}")]
        public async Task<IActionResult> UserGetUserInfo(int userRequestId)
        {
            var response = await _userService.UserGetUserInfo(userRequestId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPatch("{userRequestId}")]
        public async Task<IActionResult> UserUpdateInfo([FromForm] UserUpdateInfoDTO user, int userRequestId)
        {
            var response = await _userService.UserUpdateInfo(user, userRequestId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(user);
        }
    }
}