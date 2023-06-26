using CapstoneProjectLibrary.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneProjectLibrary.Models
{
    public class User
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AvatarUrl { get; set; }
        public UserPermissionsRoles Role { get; set; }
    }
}
