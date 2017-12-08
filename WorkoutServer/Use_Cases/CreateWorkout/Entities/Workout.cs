using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkoutServer.Use_Cases.CreateWorkout
{
    public class Workout
    {
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public Goal? goal { get; set; }
    }

    public struct Goal
    {
        public int reps { get; set; }
        public int? weights { get; set; }
    }
    
}