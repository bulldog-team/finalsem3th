using System.Threading.Tasks;
using API.DTO.User;
using API.Services.AuthService;
using API.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public AccountsController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllUsers();
            return Ok(response);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO request)
        {
            var response = await _authService.Register(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


    }
}