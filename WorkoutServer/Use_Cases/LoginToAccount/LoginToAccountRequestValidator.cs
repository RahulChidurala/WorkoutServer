using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace WorkoutServer.Use_Cases.LoginToAccount
{
    public class LoginToAccountRequestValidator: AbstractValidator<LoginToAccountRequest>
    {
        public LoginToAccountRequestValidator()
        {
            RuleFor(r => r.username).NotEmpty().WithMessage("Username cannot be empty!");
            RuleFor(r => r.password).NotEmpty().WithMessage("Password cannot be empty!");
        }
    }
}