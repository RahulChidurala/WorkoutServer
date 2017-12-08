using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace WorkoutServer.Use_Cases.CreateWorkout
{
    public class CreateWorkoutRequestValidator: AbstractValidator<CreateWorkoutRequest>
    {

        public CreateWorkoutRequestValidator()
        {
            RuleFor(r => r.workout.name).NotEmpty().WithMessage("The workout name cannot be empty");
        }
    }
}