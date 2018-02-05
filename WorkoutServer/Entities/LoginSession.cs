using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkoutServer.Entities
{
    public class LoginSession
    {
        public string Session { get; }
        public string Email { get; }
        public DateTime Timestamp { get; }
        
        public LoginSession(string session, string email, DateTime timestamp)
        {
            Session = session;
            Email = email;
            Timestamp = timestamp;
        }
    }
}