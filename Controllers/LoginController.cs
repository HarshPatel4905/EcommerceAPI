using Ecommerce_Project_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Ecommerce_Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private EcommerceDBContext _context;
        private IConfiguration _config;

        public LoginController(EcommerceDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLoginModel userLoginModel)
        {
            var user = Authenticate(userLoginModel);

            if(user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }

        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Fname),
                new Claim(ClaimTypes.NameIdentifier, user.Lname),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.Mobile),
                new Claim(ClaimTypes.Role, user.Roles.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(UserLoginModel userLoginModel)
        {
            var currentUser = _context.Users.FirstOrDefault(o => o.Email.ToLower() == userLoginModel.Email.ToLower() 
                                                            && o.Password == userLoginModel.Password);

            if(currentUser != null)
            {
                return currentUser;
            }

            return null;
        }
    }
}
