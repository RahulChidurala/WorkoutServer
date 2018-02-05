using System.Web.Http;
using WorkoutServer.Repository;
using WorkoutServer.Use_Cases.CreateAccount;
using WorkoutServer.Gateways;
using FluentValidation;
using Newtonsoft.Json;
using WorkoutServer.Entities;

namespace WorkoutServerRestAPI.Controllers
{
    public class RegisterController : ApiController
    {
        private CreateAccountInteractor interactor;

        public RegisterController(IRepository<string, Account> repo, AbstractValidator<CreateAccountRequest> validator)
        {
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