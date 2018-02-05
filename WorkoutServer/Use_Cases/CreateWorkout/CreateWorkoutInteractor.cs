using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutServer.Repository;
using WorkoutServer.MessageHandlers;
using FluentValidation;
using FluentValidation.Results;
using WorkoutServer.Entities;
using WorkoutServer.Authentication;

namespace WorkoutServer.Use_Cases.CreateWorkout
{
    public class CreateWorkoutInteractor : IRequestHandler<CreateWorkoutRequest, CreateWorkoutResponse>
    {

        private readonly IRepository<int, Workout> _repo;
        private readonly IValidator<CreateWorkoutRequest> _validator;
        private readonly IAuthentication<LoginSession> _authenticator;

        public CreateWorkoutInteractor(IRepository<int, Workout> repo, IValidator<CreateWorkoutRequest> validator, IAuthentication<LoginSession> authenticator)
        {
            _repo = repo;
            _validator = validator;
            _authenticator = authenticator;
        }

        // TODO: Make all interactors throwable?
        public CreateWorkoutResponse handle(CreateWorkoutRequest request)
        {
            var validationResult = _validator.Validate(request);

            // TODO: Move the check for valid login Session somewhere else
            // Validate user
            var loginSession = request.LoginSession;
            if(loginSession == null)
            {
                validationResult.Errors.Add(new ValidationFailure("Session", "There is no session."));
                return new CreateWorkoutResponse(validationResult, false);
            }

            if(_authenticator.IsValidSession(loginSession) == false)
            {
                validationResult.Errors.Add(new ValidationFailure("Session", "Invalid session!"));
                return new CreateWorkoutResponse(validationResult, false);
            }
                

            var goal = new Goal()
            {
                reps = request.Reps,
                weights = request.Weights
            };

            var workout = new Workout()
            {
                Name = request.Name,
                Category = request.Category,
                Goal = goal
            };
           
            if (validationResult.IsValid == false)
            {
                string errorString = "";

                foreach (ValidationFailure error in validationResult.Errors)
                {
                    errorString = errorString + "\n" + error.ErrorMessage;
                }

                validationResult.Errors.Add(getError(errorString, workout));
                return new CreateWorkoutResponse(validationResult, false);
            }

            // TODO: Move db checks inside repo? and make it throw errors
            Workout doesWorkoutExist = _repo.Retrieve(workout.Name.GetHashCode());

            if (doesWorkoutExist != null)
            {
                validationResult.Errors.Add(getError("Workout with name already exists!", workout));
                return new CreateWorkoutResponse(validationResult, false);
            }

            _repo.Create(workout);
 
            var response = new CreateWorkoutResponse(validationResult, true);

            return response;
        }

        private FluentValidation.Results.ValidationFailure getError(string message, object entity)
        {
            return new FluentValidation.Results.ValidationFailure("Exception", message, entity);
        }
    }
}
