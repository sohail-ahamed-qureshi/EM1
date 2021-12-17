using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer
{
    public class User
    {
        [Key]
        public long UserID  { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool isActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

    public class UpdateReq
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }



    public class UserReq
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class LoginReq
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserResponse
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool isActive { get; set; }
        public string Token { get; set; }
        public List<string> errors { get; set; }
    }

    public class Roles
    {
        public const string User = "User";
        public const string Admin = "Admin";
    }
}
