using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutServer.Repository;

namespace WorkoutServer.Use_Cases.CreateWorkout
{
    public class InMemoryWorkoutRepository : IRepository<int, Workout>
    {
        private static readonly Dictionary<int, Workout> store = new Dictionary<int, Workout>();

        /// <summary>
        /// Gets all workout objects.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Workout> All()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the workout object.
        /// </summary>
        /// <exception cref="WorkoutRepoException">Throws when system fails to save the workout.</exception>
        /// <param name="workout">Workout object to save.</param>
        /// <returns></returns>
        public void Create(Workout entity)
        {
            var nameHash = entity.name.GetHashCode();            
            store.Add(nameHash, entity);
        }

        public void Delete(Workout entity)
        {
            throw new NotImplementedException();
        }

        public Workout Retrieve(int id)
        {
            Workout workout;
            store.TryGetValue(id, out workout);

            return workout;
        }

        public void Update(Workout entity)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable()]
    public class WorkoutRepoException: Exception
    {

        public WorkoutRepoException() : base() { }
        public WorkoutRepoException(string message) : base(message) { }
        public WorkoutRepoException(string message, System.Exception inner) : base(message, inner) { }

        protected WorkoutRepoException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
    }
}
