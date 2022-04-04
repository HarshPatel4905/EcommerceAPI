using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Ecommerce_Project_Api.Models
{
    public partial class User
    {
        [Key]
        public int UserId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is required")]
        [Phone]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int? Roles { get; set; } = 1;
    }
}
