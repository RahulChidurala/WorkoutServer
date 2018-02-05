using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkoutServer.Repository;
using FluentValidation;
using WorkoutServerRestAPI.Controllers;
using WorkoutServer.Gateways;
using WorkoutServer.Entities;
using WorkoutServer.Use_Cases.LoginToAccount;
using Newtonsoft.Json;

namespace WorkoutServerRestAPI.Tests.Controllers
{
    [TestClass]
    public class LoginControllerTests
    {
        private IRepository<string, Account> accountRepo;
        private IRepository<string, LoginSession> sessionRepo;
        private AbstractValidator<LoginToAccountRequest> validator;
        private LoginController controller;

        [TestInitialize]
        public void Initialize()
        {
            accountRepo = new InMemoryAccountRepository();
            sessionRepo = new InMemoryLoginSessionRepository();
            validator = new LoginToAccountRequestValidator();
            controller = new LoginController(accountRepo, sessionRepo, validator);
        }

        [TestMethod]
        public void LoginController_WhenExistingEmail_ShouldResponseIsSuccess()
        {
            var account = new Account("rahul@mail.com", "password");
            accountRepo.Create(account);

            var request = new LoginToAccountRequest("rahul@mail.com", "password");

            var jsonResponse = controller.Post(request);
            var loginToAccountResponse = (LoginToAccountResponse)JsonConvert.DeserializeObject<LoginToAccountResponse>(jsonResponse);

            var sessionValue = loginToAccountResponse.LoginSession?.Session;
            Assert.IsNotNull(sessionValue);
            Assert.IsTrue(loginToAccountResponse.Success);
        }

        [TestMethod]
        public void LoginController_WhenNoExistingEmail_ShouldResponseIsNotSuccessful()
        {
            var account = new Account("rahul@mail.com", "password");
            accountRepo.Delete(account);
            var request = new LoginToAccountRequest("rahul@mail.com", "password");

            var jsonResponse = controller.Post(request);
            var loginToAccountResponse = (LoginToAccountResponse)JsonConvert.DeserializeObject<LoginToAccountResponse>(jsonResponse);

            var session = loginToAccountResponse.LoginSession;

            Assert.IsNull(session, "Session is not null!");
            Assert.IsFalse(loginToAccountResponse.Success);
        }
    }
}
