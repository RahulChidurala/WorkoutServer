using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutServer.Repository
{
    public interface IRepository<TId, TEntity> where TEntity : class
    {
        void Create(TEntity entity);
        TEntity Retrieve(TId id);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        IEnumerable<TEntity> All();
    }
}
