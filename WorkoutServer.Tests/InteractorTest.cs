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

            var workout = new Workout();
            workout.name = "Arms";
            var request = new CreateWorkoutRequest(workout);
            var response = interactor.handle(request);
            var errorCount = response.validationResult.Errors.Count;

            var retrievedWorkout = repository.Retrieve(workout.name.GetHashCode());
            Assert.AreEqual(workout, retrievedWorkout);
            Assert.AreEqual(0, errorCount);
        }

        [TestMethod]
        [ExpectedException(typeof(WorkoutRepoException))]
        public void TestInsertingDuplicateWorkout()
        {
            var repository = new InMemoryWorkoutRepository();
            var validator = new CreateWorkoutRequestValidator();
            var interactor = new CreateWorkoutInteractor(repository, validator);

            var workout = new Workout();
            workout.name = "Arms";

            // Insert into database
            repository.Create(workout);

            var request = new CreateWorkoutRequest(workout);
            CreateWorkoutResponse response = interactor.handle(request);

            var errorCount = response.validationResult.Errors.Count;
            
            Assert.AreEqual(1, errorCount);
        }
    }
}
