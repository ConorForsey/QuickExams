using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickExamLibrary.Models
{
    public class User
    {       
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public RoleType RoleType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
        public User()
        {

        }

        public User(string username, string password, string firstname, string lastname, string email, RoleType roleType)
        {
            Username = username;
            Password = password;
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            RoleType = roleType;
        }
    }
}
