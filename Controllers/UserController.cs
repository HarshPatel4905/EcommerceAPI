using Ecommerce_Project_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Ecommerce_Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("Admins")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult AdminsEndPoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.Fname}, you are an Admin");
        }

        [HttpGet("Users")]
        [Authorize(Roles = Roles.User)]
        public IActionResult UsersEndPoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.Fname}, you are an User");
        }

        [HttpGet("Public")]
        public IActionResult Public()
        {
        return Ok("Hi, you're on public property"); 
        }

        private User GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if(identity != null)
            {
                var userClaims = identity.Claims;

                return new User
                {
                    Fname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Lname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    Mobile = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.MobilePhone)?.Value,
                    Roles = int.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value),
                };
            }
            return null;
        }
    }
}
