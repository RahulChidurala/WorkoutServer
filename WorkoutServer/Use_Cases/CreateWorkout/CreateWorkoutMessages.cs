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
        // Workout properties
        public string name { get; set; }
        public string category { get; set; }

        // Goal properties
        public int reps { get; set; }
        public int? weights { get; set; }
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