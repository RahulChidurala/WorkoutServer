using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutServer.Repository;
using WorkoutServer.MessageHandlers;
using FluentValidation;

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

        public CreateWorkoutResponse handle(CreateWorkoutRequest message)
        {
            var validationResult = _validator.Validate(message);

            if (validationResult.IsValid == false)
            {
                validationResult.Errors.Add(getError("Invalid workout format!", message.workout));
                return new CreateWorkoutResponse(validationResult);
            }

            try
            {
                _repository.Create(message.workout);

            } catch(WorkoutRepoException ex)
            {
                validationResult.Errors.Add(getError(ex.Message, message.workout));
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
