using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutServer.Repository;
using WorkoutServer.MessageHandlers;
using FluentValidation;
using FluentValidation.Results;

namespace WorkoutServer.Use_Cases.CreateWorkout
{
    public class CreateWorkoutInteractor : IRequestHandler<CreateWorkoutRequest, CreateWorkoutResponse>
    {

        private readonly IRepository<int, Workout> _repository;
        private readonly IValidator<CreateWorkoutRequest> _validator;

        public CreateWorkoutInteractor(IRepository<int, Workout> repository, IValidator<CreateWorkoutRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public CreateWorkoutResponse handle(CreateWorkoutRequest request)
        {
            var validationResult = _validator.Validate(request);

            var goal = new Goal()
            {
                reps = request.reps,
                weights = request.weights
            };

            var workout = new Workout()
            {
                name = request.name,
                category = request.category,
                goal = goal
            };
           
            if (validationResult.IsValid == false)
            {
                string errorString = "";

                foreach (ValidationFailure error in validationResult.Errors)
                {
                    errorString = errorString + "\n" + error.ErrorMessage;
                }

                validationResult.Errors.Add(getError(errorString, workout));
                return new CreateWorkoutResponse(validationResult);
            }

            Workout doesWorkoutExist = _repository.Retrieve(workout.name.GetHashCode());

            if (doesWorkoutExist != null)
            {
                validationResult.Errors.Add(getError("Workout with name already exists!", workout));
                return new CreateWorkoutResponse(validationResult);
            }
 
            var response = new CreateWorkoutResponse(validationResult);

            return response;
        }

        private FluentValidation.Results.ValidationFailure getError(string message, object entity)
        {
            return new FluentValidation.Results.ValidationFailure("Exception", message, entity);
        }
    }
}
