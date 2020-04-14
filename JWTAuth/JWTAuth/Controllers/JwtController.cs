using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWTAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JWTAuth.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        private IConfiguration _config;

        public JwtController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [Route("auth/login")]
        [HttpPost]
        public IActionResult Login([FromBody]User user)
        {
            var isValidUser = AuthenticateUser(user);
            if (isValidUser)
            {
                var token = GenerateJWT(user);
                return Ok(new { token });
            }
            else
                return Unauthorized();
        }

        private string GenerateJWT(User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"])), SecurityAlgorithms.HmacSha256),
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }

        private bool AuthenticateUser(User user)
        {
            //For demo using static user information
            if (user.Email == "admin@test.com" && user.Password == "admin")
            {
                return true;
            }
            else
                return false;
        }

        [Route("user")]
        [HttpGet]
        public List<string> GetUsers()
        {
            var users = new List<string>
            {
                "Value1",
                "Value2",
                "Value3"
            };
            return users;
        }
    }
}