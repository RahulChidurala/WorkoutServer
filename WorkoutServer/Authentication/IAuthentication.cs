using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutServer.Repository;

namespace WorkoutServer.Authentication
{
    public interface IAuthentication<TSession>
    {
        bool IsValidSession(TSession session);
    }
}
