using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhatToWatch.Business.Abstract;
using WhatToWatch.Entities.Dtos.User;

namespace WhatToWatch.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        //Authorize test
        [HttpGet("test")]
        public IActionResult Login()
        {
            return Ok("test ok");
        }
    }
}
