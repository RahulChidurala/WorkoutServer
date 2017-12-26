using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkoutServer.MessageHandlers;

namespace WorkoutServer.Use_Cases.LoginToAccount
{
    public class LoginToAccountInteractor: IRequestHandler<LoginToAccountRequest, LoginToAccountResponse>
    {

        public LoginToAccountInteractor()
        {

        }

        public LoginToAccountResponse handle(LoginToAccountRequest message)
        {
            throw new NotImplementedException();
        }
    }
}