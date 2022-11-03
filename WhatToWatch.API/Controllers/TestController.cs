using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WhatToWatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public ActionResult Get() =>Ok("success");
    }
}
