using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Customer : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            var zx= HttpContext.User.Claims.FirstOrDefault(c => c.Type == "NationCode");
            var list = new[] {"Mohammad", "Donya", "Maya"};
            return list;
        }

    }


}
