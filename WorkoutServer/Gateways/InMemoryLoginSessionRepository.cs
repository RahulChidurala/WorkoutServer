using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkoutServer.Repository;
using WorkoutServer.Entities;
using Email = System.String;

namespace WorkoutServer.Gateways
{
    public class InMemoryLoginSessionRepository : IRepository<Email, LoginSession>
    {
        private readonly Dictionary<Email, LoginSession> repo = new Dictionary<Email, LoginSession>();

        public IEnumerable<LoginSession> All()
        {
            throw new NotImplementedException();
        }

        public void Create(LoginSession entity)
        {
            repo.Add(entity.Email, entity);
        }

        public void Delete(LoginSession entity)
        {
            repo.Remove(entity.Email);
        }

        public LoginSession Retrieve(Email id)
        {
            LoginSession loginSession;
            repo.TryGetValue(id, out loginSession);
            return loginSession;
        }

        public void Update(LoginSession entity)
        {
            repo[entity.Email] = entity;
        }
    }
}