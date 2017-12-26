using System.Web.Http;
using WorkoutServer.Repository;
using WorkoutServer.Use_Cases.CreateAccount;
using WorkoutServer.Use_Cases.CreateAccount.Gateways;
using WorkoutServer.Use_Cases.CreateAccount.Entities;
using FluentValidation;
using Newtonsoft.Json;

namespace WorkoutServerRestAPI.Controllers
{
    public class RegisterController : ApiController
    {
        private IRepository<string, Account> repo;
        private AbstractValidator<CreateAccountRequest> validation;
        private CreateAccountInteractor interactor;

        public RegisterController()
        {
            repo = new InMemoryAccountRepository();
            validation = new CreateAccountRequestValidator();
            interactor = new CreateAccountInteractor(repo, validation);
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody]AccountModel account)
        {
            var username = account.username;
            var password = account.password;
            CreateAccountRequest request = new CreateAccountRequest(username, password);

            var response = interactor.handle(request);
            var jsonResponse = JsonConvert.SerializeObject(response, Formatting.Indented);

            return jsonResponse;
        }
    }

    public class AccountModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}