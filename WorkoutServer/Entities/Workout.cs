using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkoutServer.Entities
{
    public class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public Goal? Goal { get; set; }
        public Account Author { get; set; }
    }

    public struct Goal
    {
        public int reps { get; set; }
        public int? weights { get; set; }
    }
    
}