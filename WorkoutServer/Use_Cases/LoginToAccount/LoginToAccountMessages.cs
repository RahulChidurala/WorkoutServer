using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkoutServer.Messages;
using FluentValidation.Results;
using WorkoutServer.Entities;

namespace WorkoutServer.Use_Cases.LoginToAccount
{
    public class LoginToAccountRequest: IRequest<LoginToAccountResponse>
    {
        public String Email { get; set; }
        public String Password { get; set; }

        public LoginToAccountRequest() { }

        public LoginToAccountRequest(String email, String password)
        {
            this.Email = email;
            this.Password = password;
        }
    }

    public class LoginToAccountResponse
    {

        public bool Success { get; set; }
        public String Email { get; set; }
        public LoginSession LoginSession { get; set; }
        public ValidationResult ValidationResult { get; set; }

        public LoginToAccountResponse() { }

        public LoginToAccountResponse(string email, ValidationResult validationResult)
        {
            Email = email;
            ValidationResult = validationResult;
        }
    }
}