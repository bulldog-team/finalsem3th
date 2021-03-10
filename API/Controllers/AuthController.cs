using System.Threading.Tasks;
using API.DTO.User;
using API.Services.AuthService;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO request)
        {
            var response = await _authRepo.Login(request.Username, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            else return Ok(response);
        }
    }
}