using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WorkoutServer.Use_Cases.LoginToAccount
{
    public class ISession<T>
    {
        public T session;
        public DateTime timestamp;

        public ISession(T session, DateTime timestamp)
        {
            this.session = session;
            this.timestamp = timestamp;
        }
    }
}