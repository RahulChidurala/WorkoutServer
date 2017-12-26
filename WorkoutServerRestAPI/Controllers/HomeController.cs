using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace WorkoutServerRestAPI.Controllers
{
    public class HomeController : ApiController
    {
        public string Get()
        {
            return "Welcome";
        }
    }
}
