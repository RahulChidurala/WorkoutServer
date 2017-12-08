using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkoutServer.Use_Cases.CreateWorkout;

namespace WorkoutServer.Tests
{
    [TestClass]
    public class InteractorTest
    {
        [TestMethod]
        public void TestCreateNewWorkout()
        {
            var repository = new InMemoryWorkoutRepository();
            var validator = new CreateWorkoutRequestValidator();
            var interactor = new CreateWorkoutInteractor(repository, validator);

            var request = new CreateWorkoutRequest();
            request.name = "Arms";
            var response = interactor.handle(request);
            var errorCount = response.validationResult.Errors.Count;

            var retrievedWorkout = repository.Retrieve(request.name.GetHashCode());
            Assert.AreEqual(0, errorCount);
        }

        [TestMethod]
        public void TestInsertingDuplicateWorkout()
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
