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
            var model = new AccountModel();
            model.username = "user1@email.com";
            model.password = "password1";
            var response = controller.Post(model);

            var createAccountResponse = (CreateAccountResponse)JsonConvert.DeserializeObject<CreateAccountResponse>(response);

            Assert.IsTrue(createAccountResponse.Success);
        }

        [TestMethod]
        public void CreateAccount_WhenExistingUsername_ShouldResponseIsNotSuccesful()
        {
            // Insert first user
            var model = new AccountModel();
            model.username = "user1@email.com";
            model.password = "password1";
            controller.Post(model);

            // Insert second user
            var response = controller.Post(model);

            var createAccountResponse = (CreateAccountResponse)JsonConvert.DeserializeObject<CreateAccountResponse>(response);

            Assert.IsFalse(createAccountResponse.Success);
        }

        [TestMethod]
        public void CreateAccount_WhenInvalidUsername_ShouldInvalidUsername()
        {
            var model = new AccountModel();
            model.username = "user1NotEmail";
            model.password = "password1";
            var response = controller.Post(model);

            var createAccountResponse = (CreateAccountResponse)JsonConvert.DeserializeObject<CreateAccountResponse>(response);

            Assert.IsFalse(createAccountResponse.validationResult.IsValid);
        }
    }
}
