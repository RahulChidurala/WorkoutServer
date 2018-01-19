using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkoutServer.Repository;
using WorkoutServer.Use_Cases.CreateAccount.Gateways;
using WorkoutServerRestAPI.Controllers;
using Newtonsoft.Json;
using FluentValidation;
using WorkoutServer.Use_Cases.CreateAccount;
using WorkoutServer.Use_Cases.CreateAccount.Entities;

namespace WorkoutServerRestAPI.Tests.Controllers
{
    [TestClass]
    public class RegisterControllerTest
    {

        private IRepository<string, Account> repo;
        private AbstractValidator<CreateAccountRequest> validator;
        private RegisterController controller;

        [TestInitialize]
        public void Initialize()
        {
            repo = new InMemoryAccountRepository();
            validator = new CreateAccountRequestValidator();
            controller = new RegisterController(repo, validator);
        }

        [TestMethod]
        public void CreateAccount_WhenNoExistingUsername_ShouldResponseIsSuccess()
        {
            var request = new CreateAccountRequest();
            request.email = "user1@email.com";
            request.password = "password1";
            var response = controller.Post(request);

            var createAccountResponse = (CreateAccountResponse)JsonConvert.DeserializeObject<CreateAccountResponse>(response);

            Assert.IsTrue(createAccountResponse.Success);
        }

        [TestMethod]
        public void CreateAccount_WhenExistingUsername_ShouldResponseIsNotSuccesful()
        {
            // Insert first user
            var request = new CreateAccountRequest();
            request.email = "user1@email.com";
            request.password = "password1";
            controller.Post(request);

            // Insert second user
            var response = controller.Post(request);

            var createAccountResponse = (CreateAccountResponse)JsonConvert.DeserializeObject<CreateAccountResponse>(response);

            Assert.IsFalse(createAccountResponse.Success);
        }

        [TestMethod]
        public void CreateAccount_WhenInvalidUsername_ShouldInvalidUsername()
        {
            var request = new CreateAccountRequest();
            request.email = "user1";
            request.password = "password1";
            var response = controller.Post(request);

            var createAccountResponse = (CreateAccountResponse)JsonConvert.DeserializeObject<CreateAccountResponse>(response);

            Assert.IsFalse(createAccountResponse.validationResult.IsValid);
        }
    }
}
