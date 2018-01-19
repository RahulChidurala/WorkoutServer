using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkoutServer.Messages;

namespace WorkoutServer.Use_Cases.CreateAccount
{
    public class CreateAccountRequest : IRequest<CreateAccountResponse>
    {
        public string email { get; set; }
        public string password { get; set; }        
    }

    public class CreateAccountResponse
    {
        public bool Success { get; set; }        
        public ValidationResult validationResult { get; set; }
        
        public CreateAccountResponse(ValidationResult validationResult)
        {
            this.validationResult = validationResult;
        }
    }
}