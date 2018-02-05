using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkoutServer.Entities;
using WorkoutServer.Repository;
using Email = System.String;

namespace WorkoutServer.Authentication
{
    /**
     * Session also validates LoginSession.
     */
    public class AuthenticationService : IAuthentication<LoginSession>
    {
        private IRepository<Email, LoginSession> _repo;

        public AuthenticationService(IRepository<Email, LoginSession> repo)
        {
            _repo = repo;
        }

        public bool IsValidSession(LoginSession session)
        {
            // TODO: Add time limit
            var retrievedSession = _repo.Retrieve(session.Email);
            if (retrievedSession == null)
                return false;
            else
                return true;
        }
    }
}