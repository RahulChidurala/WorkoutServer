using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkoutServer.Entities;
using WorkoutServer.Repository;

namespace WorkoutServer.Gateways
{
    /**
     * IRepository<string, Account>. String is the primary key.
     */
    public class InMemoryAccountRepository : IRepository<string, Account>
    {
        private readonly Dictionary<string, Account> repo = new Dictionary<string, Account>();

        public IEnumerable<Account> All()
        {
            throw new NotImplementedException();
        }

        public void Create(Account entity)
        {
            repo.Add(entity.Email, entity);
        }

        public void Delete(Account entity)
        {
            repo.Remove(entity.Email);
        }

        public Account Retrieve(string id)
        {
            Account account;
            repo.TryGetValue(id, out account);

            return account;
        }

        public void Update(Account entity)
        {
            repo[entity.Email] = entity;
        }
    }
}