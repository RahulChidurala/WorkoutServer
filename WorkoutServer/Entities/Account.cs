using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkoutServer.Entities
{
    public class Account
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public Account() {  }

        public Account(string email, string password)
        {
            Email = email;
            Password = password;
        }        
    }
}