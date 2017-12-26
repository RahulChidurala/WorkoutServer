using System.Web.Http;
using WorkoutServer.Repository;
using WorkoutServer.Use_Cases.CreateAccount;
using WorkoutServer.Use_Cases.CreateAccount.Gateways;
using WorkoutServer.Use_Cases.CreateAccount.Entities;
using FluentValidation;

namespace WorkoutServerRestAPI.Controllers
{
    
    public class LoginController : ApiController
    {
        private IRepository<string, Account> repo;
        private AbstractValidator<CreateAccountRequest> validation;
        private CreateAccountInteractor interactor;

        public LoginController()
        {
            repo = new InMemoryAccountRepository();
            validation = new CreateAccountRequestValidator();
            interactor = new CreateAccountInteractor(repo, validation);
        }

        // POST api/<controller>
        public void Post([FromBody]string username, [FromBody]string password)
        {
            
        }
    }
}