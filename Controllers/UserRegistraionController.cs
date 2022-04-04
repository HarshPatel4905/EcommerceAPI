using Ecommerce_Project_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Ecommerce_Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistraionController : ControllerBase
    {
        private EcommerceDBContext _context;

        public UserRegistraionController(EcommerceDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterUser(User user)
        {
            try
            {
                user.Roles = 1;
                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok("the user "+user.Fname+user.Lname+"is added successfully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
