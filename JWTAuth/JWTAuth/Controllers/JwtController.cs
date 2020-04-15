using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

        public List<User> users = new List<User>()
        {
            new User{ email = "superAdmin@test.com", password = "super", role = Roles.SuperAdmin},
            new User{ email = "admin@test.com", password = "admin", role = Roles.Admin},
            new User{ email = "user@test.com", password = "user", role = Roles.User},
        };

        public JwtController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [Route("auth/login")]
        [HttpPost]
        public IActionResult Login([FromBody]User user)
        {
            var userData = users.Where(x => x.email.ToLower() == user.email.ToLower() && x.password == user.password).FirstOrDefault();

            if (userData != null)
            {
                var token = GenerateJWT(userData);
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
                    new Claim(ClaimTypes.Email, user.email),
                    new Claim(ClaimTypes.Role, user.role)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"])), SecurityAlgorithms.HmacSha256),
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }

        [Authorize(Roles = Roles.User)]
        [Route("user")]
        [HttpGet]
        public List<string> GetDataForUsers()
        {
            var data = new List<string>
            {
                "userData1",
                "userData2",
                "userData3"
            };
            return data;
        }

        [Authorize(Roles = Roles.SuperAdmin)]
        [Route("superAdmin")]
        [HttpGet]
        public List<string> GetDataForSuperAdmin()
        {
            var data = new List<string>
            {
                "SuperAdmin1",
                "SuperAdmin2",
                "SuperAdmin3"
            };
            return data;
        }

        [Authorize(Roles = Roles.Admin)]
        [Route("admin")]
        [HttpGet]
        public List<string> GetDataForAdmin()
        {
            var data = new List<string>
            {
                "Admin1",
                "Admin2",
                "Admin3"
            };
            return data;
        }
    }
}