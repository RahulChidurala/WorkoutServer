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
            RuleFor(r => r.Email).NotEmpty().WithMessage("Email cannot be empty!");
            RuleFor(r => r.Password).NotEmpty().WithMessage("Password cannot be empty!");
        }
    }
}