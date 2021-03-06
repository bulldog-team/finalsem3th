using System.Threading.Tasks;
using API.DTO.User;
using API.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authRepo;
        public AuthController(IAuthService authRepo)
        {
            _authRepo = authRepo;
        }

        // Login route
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO request)
        {
            var response = await _authRepo.Login(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            else return Ok(response);
        }

        // Admin create a new user route
        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO request)
        {
            var response = await _authRepo.Register(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            else return Ok(response);
        }
    }
}