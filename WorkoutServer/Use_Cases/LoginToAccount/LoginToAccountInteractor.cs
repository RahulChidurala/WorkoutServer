using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkoutServer.Entities;
using WorkoutServer.MessageHandlers;
using WorkoutServer.Repository;

namespace WorkoutServer.Use_Cases.LoginToAccount
{
    public class LoginToAccountInteractor: IRequestHandler<LoginToAccountRequest, LoginToAccountResponse>
    {
        private readonly IRepository<string, Account> _accountRepo;
        private readonly IRepository<string, LoginSession> _sessionRepo;
        private readonly IValidator<LoginToAccountRequest> _validator;

        public LoginToAccountInteractor(IRepository<string, Account> accountRepo, IRepository<string, LoginSession> sessionRepo, IValidator<LoginToAccountRequest> validator)
        {
            _accountRepo = accountRepo;
            _sessionRepo = sessionRepo;
            _validator = validator;
        }

        public LoginToAccountResponse handle(LoginToAccountRequest request)
        {
            var validationResult = _validator.Validate(request);

            var loginToAccount = new Account() {
                Email = request.Email,
                Password = request.Password
            };

            if (validationResult.IsValid == false)
            {
                string errorString = "";

                foreach (ValidationFailure error in validationResult.Errors)
                {
                    errorString = errorString + "\n" + error.ErrorMessage;
                }
                

                loginToAccount.Password = "HIDDEN";
                validationResult.Errors.Add(GetError(errorString, loginToAccount));
                var errorResponse = new LoginToAccountResponse() {
                    
                    Email = request.Email,
                    ValidationResult = validationResult
                };
                return errorResponse;
            }

            Account doesAccountExist = _accountRepo.Retrieve(loginToAccount.Email);

            if (doesAccountExist == null)
            {
                validationResult.Errors.Add(GetError("User does not exist!", loginToAccount));
                return new LoginToAccountResponse()
                {
                    Email = loginToAccount.Email,
                    ValidationResult = validationResult
                };
            }

            var response = new LoginToAccountResponse(loginToAccount.Email, validationResult);

            // Create session
            // TODO: Make a more secure session value
            var sessionValue = DateTime.Now.GetHashCode().ToString();
            var loginSession = new LoginSession(sessionValue, response.Email, DateTime.Now);
            _sessionRepo.Create(loginSession);

            response.LoginSession = loginSession;
            response.Success = true;

            return response;
        }

        private FluentValidation.Results.ValidationFailure GetError(string message, object entity)
        {
            return new FluentValidation.Results.ValidationFailure("Exception", message, entity);
        }
    }    
}