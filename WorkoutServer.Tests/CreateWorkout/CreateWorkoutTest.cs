using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkoutServer.Use_Cases.CreateWorkout;
using WorkoutServer.Repository;
using FluentValidation;

namespace WorkoutServer.Tests
{
    [TestClass]
    public class InteractorTest
    {

        private IRepository<int, Workout> repo;
        private AbstractValidator<CreateWorkoutRequest> validator;
        private CreateWorkoutInteractor interactor;

        [TestInitialize]
        public void Initialize()
        {
            repo = new InMemoryWorkoutRepository();
            validator = new CreateWorkoutRequestValidator();
            interactor = new CreateWorkoutInteractor(repo, validator);
        }

        [TestMethod]
        public void CreateNewWorkout_WhenValidNewWorkout_ShouldBe0Errors()
        {
            var request = new CreateWorkoutRequest();
            request.name = "Arms";
            var response = interactor.handle(request);
            var errorCount = response.validationResult.Errors.Count;

            var retrievedWorkout = repo.Retrieve(request.name.GetHashCode());
            Assert.AreEqual(0, errorCount);
        }

        [TestMethod]
        public void CreateNewWorkout_WhenDuplicateWorkout_ShouldBe1Errors()
        {
            var repository = new InMemoryWorkoutRepository();
            var validator = new CreateWorkoutRequestValidator();
            var interactor = new CreateWorkoutInteractor(repository, validator);

            var workout = new Workout();
            workout.name = "Arms";

            // Insert into database
            repository.Create(workout);

            var request = new CreateWorkoutRequest();
            request.name = "Arms";
            CreateWorkoutResponse response = interactor.handle(request);

            var errorCount = response.validationResult.Errors.Count;
            
            Assert.AreEqual(1, errorCount);
        }
    }
}
