using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkoutServer.Use_Cases.CreateAccount
{
    public class CreateAccountRequestValidator: AbstractValidator<CreateAccountRequest>
    {
        public CreateAccountRequestValidator()
        {
            RuleFor(r => r.email).NotEmpty().WithMessage("Email cannot be empty!");
            RuleFor(r => r.email).EmailAddress().WithMessage("Invalid email!");
            RuleFor(r => r.password).NotEmpty().WithMessage("Password cannot be empty!");
        }
    }
}