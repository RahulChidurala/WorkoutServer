using System.Web.Http;
using Unity;
using Unity.WebApi;
using FluentValidation;
using WorkoutServer.Repository;
using WorkoutServer.Use_Cases.CreateAccount;
using WorkoutServer.Use_Cases.CreateAccount.Gateways;
using WorkoutServer.Use_Cases.CreateAccount.Entities;

namespace WorkoutServerRestAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IRepository<string, Account>, InMemoryAccountRepository>();
            container.RegisterType<AbstractValidator<CreateAccountRequest>, CreateAccountRequestValidator>();
                
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

        }
    }
}