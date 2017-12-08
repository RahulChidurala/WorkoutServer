using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkoutServer.Repository;
using WorkoutServer.Use_Cases.CreateAccount.Entities;

namespace WorkoutServer.Use_Cases.CreateAccount.Gateways
{
    public class InMemoryAccountRepository : IRepository<string, Account>
    {
        private static readonly Dictionary<string, Account> repo = new Dictionary<string, Account>();

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
            throw new NotImplementedException();
        }

        public Account Retrieve(string id)
        {
            Account account;
            repo.TryGetValue(id, out account);

            return account;
        }

        public void Update(Account entity)
        {
            throw new NotImplementedException();
        }
    }
}