using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthenticationController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]
        public string Index()
        {
            return "hello";
        }
        [HttpPost,Route("Login")]
        public IActionResult Login([FromBody] LoginModel request)
        {
            if (User == null)
                return BadRequest("Invalid client request!");
            if (request.UserName == "mohammad" && request.Password == "123")
            {
                var sequrity = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var signinCredentials = new SigningCredentials(sequrity, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:44334",
                    audience: "https://localhost:44334",
                    claims: new List<Claim>()
                    {
                        new Claim("NationCode",value:request.UserName)
                    },
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials

                    );
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { token = token });

            }

            return Unauthorized();

        }
    }
}
