using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation.Results;
using WorkoutServer.Messages;

namespace WorkoutServer.Use_Cases.CreateWorkout
{
    public class CreateWorkoutRequest: IRequest<CreateWorkoutResponse>
    {
        public Workout workout { get; set; }

        public CreateWorkoutRequest(Workout workout)
        {
            this.workout = workout;
        }
    }

    public class CreateWorkoutResponse
    {
        public ValidationResult validationResult { get; }

        public CreateWorkoutResponse(ValidationResult validationResult)
        {
            this.validationResult = validationResult;
        }
    }
}