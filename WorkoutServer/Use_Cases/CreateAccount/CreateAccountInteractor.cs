using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkoutServer.Repository;
using WorkoutServer.MessageHandlers;
using WorkoutServer.Use_Cases.CreateAccount.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace WorkoutServer.Use_Cases.CreateAccount
{
    public class CreateAccountInteractor : IRequestHandler<CreateAccountRequest, CreateAccountResponse>
    {

        private IRepository<string, Account> repository;
        private IValidator<CreateAccountRequest> validator;

        public CreateAccountInteractor(IRepository<string, Account> repository, IValidator<CreateAccountRequest> validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        public CreateAccountResponse handle(CreateAccountRequest message)
        {
            var validationResult = validator.Validate(message);
            CreateAccountResponse response = new CreateAccountResponse(validationResult);

            if (validationResult.IsValid == false)
            {
                string errorString = "";

                foreach(ValidationFailure error in validationResult.Errors) {
                    errorString = errorString + "\n" + error.ErrorMessage;
                }
                
                validationResult.Errors.Add(getError(errorString));
                response = new CreateAccountResponse(validationResult)
                {
                    Success = false
                };
                return response;
            }

            var existingEmail = repository.Retrieve(message.email);
            if (existingEmail != null)
            {
                validationResult.Errors.Add(getError("An account with this email already exists."));
                response = new CreateAccountResponse(validationResult)
                {
                    Success = false
                };
                return response;
            }

            var account = new Account(message.email, message.password);
            saveAccount(account);
            response.Success = true;

            return response;
        }

        private ValidationFailure getError(string message)
        {
            var validationFailure = new ValidationFailure("CreateAccount", message);

            return validationFailure;
        }

        private void saveAccount(Account account)
        {
            repository.Create(account);
        }
    }
}