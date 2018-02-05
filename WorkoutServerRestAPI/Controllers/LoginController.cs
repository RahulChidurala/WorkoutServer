using System.Web.Http;
using WorkoutServer.Repository;
using FluentValidation;
using WorkoutServer.Entities;
using WorkoutServer.Use_Cases.LoginToAccount;
using WorkoutServer.Gateways;
using Newtonsoft.Json;

namespace WorkoutServerRestAPI.Controllers
{
    
    public class LoginController : ApiController
    {
        private LoginToAccountInteractor _interactor;

        public LoginController(IRepository<string, Account> accountRepo, IRepository<string, LoginSession> sessionRepo, AbstractValidator<LoginToAccountRequest> validator)
        {
            _interactor = new LoginToAccountInteractor(accountRepo, sessionRepo, validator);
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody]LoginToAccountRequest request)
        {
            var response = _interactor.handle(request);
            var jsonResponse = JsonConvert.SerializeObject(response, Formatting.None);

            return jsonResponse;
        }
    }
}