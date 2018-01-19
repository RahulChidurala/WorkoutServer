using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkoutServer.Use_Cases.CreateAccount;
using WorkoutServer.Use_Cases.CreateAccount.Gateways;
using FluentValidation;
using WorkoutServer.Repository;
using WorkoutServer.Use_Cases.CreateAccount.Entities;

namespace WorkoutServer.Tests.CreateAccount
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class CreateAccountTest
    {

        private IRepository<string, Account> repo;
        private AbstractValidator<CreateAccountRequest> validator;
        private CreateAccountInteractor interactor;

        [TestInitialize]
        public void Initialize()
        {
            repo = new InMemoryAccountRepository();
            validator = new CreateAccountRequestValidator();
            interactor = new CreateAccountInteractor(repo, validator);
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestCreatingValidAccount()
        {
            var createAccountRequest = new CreateAccountRequest();
            createAccountRequest.email = "rahul@mail.com";
            createAccountRequest.password = "password";

            var response = interactor.handle(createAccountRequest);

            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void TestCreatingDuplicateAccount()
        {
            var createAccountRequest = new CreateAccountRequest();
            createAccountRequest.email = "rahul@mail.com";
            createAccountRequest.password = "password";

            var response = interactor.handle(createAccountRequest);
            var response2 = interactor.handle(new CreateAccountRequest{
                    email = "rahul@mail.com",
                    password = "beep"
                });

            Assert.IsTrue(response2.Success == false);
            Assert.AreEqual(1, response2.validationResult.Errors.Count);
        }
    }
}
