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
        private AbstractValidator<CreateAccountRequest> validator;
        private CreateAccountInteractor interactor;

        public RegisterController(IRepository<string, Account> repo, AbstractValidator<CreateAccountRequest> validator)
        {
            this.repo = repo;
            this.validator = validator;
            this.interactor = new CreateAccountInteractor(repo, validator);
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody]CreateAccountRequest request)
        {            
            var response = interactor.handle(request);
            var jsonResponse = JsonConvert.SerializeObject(response, Formatting.None);

            return jsonResponse;
        }
    }
}