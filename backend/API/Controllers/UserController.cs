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

        [HttpPatch]
        public async Task<IActionResult> UserUpdateInfo([FromForm] UserUpdateInfoDTO user)
        {
            var response = await _userService.UserUpdateInfo(user);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(user);
        }



    }
}