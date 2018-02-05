using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkoutServer.Use_Cases.CreateWorkout;
using WorkoutServer.Repository;
using FluentValidation;
using WorkoutServer.Entities;
using WorkoutServer.Gateways;
using WorkoutServer.Authentication;
using WorkoutServer.Use_Cases.LoginToAccount;

namespace WorkoutServer.Tests
{
    [TestClass]
    public class InteractorTest
    {

        // CreateWorkout
        private IRepository<int, Workout> workoutRepo;
        private AbstractValidator<CreateWorkoutRequest> workoutRequestValidator;
        private CreateWorkoutInteractor workoutInteractor;

        // LoginToAccount
        private IRepository<string, Account> accountRepo;
        private LoginToAccountRequestValidator loginRequestValidator;
        private LoginToAccountInteractor loginInteractor;

        // Authenticator
        private IRepository<string, LoginSession> sessionRepo;
        private IAuthentication<LoginSession> authenticator;

        [TestInitialize()]
        public void Initialize()
        {
            // Authenticator
            sessionRepo = new InMemoryLoginSessionRepository();
            authenticator = new AuthenticationService(sessionRepo);

            // LoginToAccount
            accountRepo = new InMemoryAccountRepository();
            loginRequestValidator = new LoginToAccountRequestValidator();
            loginInteractor = new LoginToAccountInteractor(accountRepo, sessionRepo, loginRequestValidator);
            
            // CreateWorkout
            workoutRepo = new InMemoryWorkoutRepository();
            workoutRequestValidator = new CreateWorkoutRequestValidator();
            sessionRepo = new InMemoryLoginSessionRepository();
            workoutInteractor = new CreateWorkoutInteractor(workoutRepo, workoutRequestValidator, authenticator);

            // Create account for testing
            var account = new Account("rahul@mail.com", "password1");
            accountRepo.Create(account);
        }

        // TODO: Replace errors with exceptions
        [TestMethod]
        public void CreateNewWorkout_WhenValidSessionAndValidNewWorkout_ShouldBe0Errors()
        {
            // Login to get session
            var loginRequest = new LoginToAccountRequest()
            {
                Email = "rahul@mail.com",
                Password = "password1"
            };

            var loginResponse = loginInteractor.handle(loginRequest);

            var request = new CreateWorkoutRequest
            {
                Name = "Arms",
                LoginSession = loginResponse.LoginSession
            };

            var response = workoutInteractor.handle(request);
            var errorCount = response.ValidationResult.Errors.Count;

            Assert.AreEqual(0, errorCount);
            Assert.IsTrue(response.Success);

            var retrievedWorkout = workoutRepo.Retrieve(request.Name.GetHashCode());            
            Assert.IsNotNull(retrievedWorkout);
        }

        // TODO: Throw errors and check for those!!!
        [TestMethod]
        public void CreateNewWorkout_WhenValidSessionAndDuplicateWorkout_ShouldBe1Errors()
        {
            // Login to get session
            var loginRequest = new LoginToAccountRequest()
            {
                Email = "rahul@mail.com",
                Password = "password1"
            };

            var loginResponse = loginInteractor.handle(loginRequest);

            var workout = new Workout
            {
                Name = "Arms"
            };

            // Insert into database
            workoutRepo.Create(workout);

            // CreateWorkout request
            var request = new CreateWorkoutRequest
            {
                Name = "Arms",
                LoginSession = loginResponse.LoginSession
            };
            CreateWorkoutResponse response = workoutInteractor.handle(request);

            var errorCount = response.ValidationResult.Errors.Count;            
            Assert.AreEqual(1, errorCount);

            var errorString = response.ValidationResult.Errors[0].ToString();

            // TODO: Throw errors and check for those!!!
            Assert.IsTrue(errorString.Contains("Workout with name already exists!"));
        }

        // TODO: Throw errors and check for those!!!
        [TestMethod]
        public void CreateNewWorkout_WhenNoSessionAndValidNewWorkout_ShouldBe1Errors()
        {

            var request = new CreateWorkoutRequest
            {
                Name = "Arms",
                LoginSession = null
            };

            var response = workoutInteractor.handle(request);
            var errorCount = response.ValidationResult.Errors.Count;

            Assert.IsFalse(response.Success);
            Assert.AreEqual(1, errorCount);

            var errorString = response.ValidationResult.Errors[0].ToString();
            // TODO: Throw errors and check for those!!!
            Assert.IsTrue(errorString.Contains("There is no session"));            
        }
    }
}
