using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkoutServer.Messages;
using FluentValidation.Results;

namespace WorkoutServer.Use_Cases.LoginToAccount
{
    public class LoginToAccountRequest: IRequest<LoginToAccountResponse>
    {
        public String username { get; }
        public String password { get; }

        public LoginToAccountRequest(String username, String password)
        {
            this.username = username;
            this.password = password;
        }
    }

    public class LoginToAccountResponse
    {

        public String username { get; }

        public ValidationResult validationResult { get; }

        public LoginToAccountResponse(String username, ValidationResult validationResult)
        {

            this.username = username;
            this.validationResult = validationResult;
        }

    }
}