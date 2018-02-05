using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutServer.Entities;
using WorkoutServer.Repository;

namespace WorkoutServer.Gateways
{
    public class InMemoryWorkoutRepository : IRepository<int, Workout>
    {
        private readonly Dictionary<int, Workout> store = new Dictionary<int, Workout>();

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
            var nameHash = entity.Name.GetHashCode();            
            store.Add(nameHash, entity);
        }

        public void Delete(Workout entity)
        {
            store.Remove(entity.Name.GetHashCode());
        }

        public Workout Retrieve(int id)
        {
            Workout workout;
            store.TryGetValue(id, out workout);

            return workout;
        }

        public void Update(Workout entity)
        {
            store[entity.Name.GetHashCode()] = entity;
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
