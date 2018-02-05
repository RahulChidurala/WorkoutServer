using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation.Results;
using WorkoutServer.Messages;
using WorkoutServer.Entities;

namespace WorkoutServer.Use_Cases.CreateWorkout
{
    public class CreateWorkoutRequest: IRequest<CreateWorkoutResponse>
    {
        // Session authentication
        public LoginSession LoginSession { get; set; }

        // Workout properties
        public string Name { get; set; }
        public string Category { get; set; }

        // Goal properties
        public int Reps { get; set; }
        public int? Weights { get; set; }
    }

    public class CreateWorkoutResponse
    {
        public ValidationResult ValidationResult { get; }
        public bool Success { get; }

        public CreateWorkoutResponse(ValidationResult validationResult, bool success)
        {
            this.ValidationResult = validationResult;
            this.Success = success;
        }
    }
}