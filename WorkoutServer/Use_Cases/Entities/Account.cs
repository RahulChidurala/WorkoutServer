using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkoutServer.Use_Cases.CreateAccount.Entities
{
    public class Account
    {
        public string Email { get; }
        public string Password { get; }

        public Account(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }
    }
}