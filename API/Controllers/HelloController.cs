
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Hello
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        public HelloController()
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("hello");
        }
    }
}
