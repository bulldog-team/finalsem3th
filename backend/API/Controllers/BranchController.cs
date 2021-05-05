using System.Threading.Tasks;
using API.Services.BranchService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        // Get branch data
        [Authorize(Roles = "Admin, User")]
        [HttpGet("info")]
        public async Task<IActionResult> GetBranchData()
        {
            var response = await _branchService.GetBranchData();
            return Ok(response);
        }
    }
}