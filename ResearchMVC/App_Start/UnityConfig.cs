using ResearchMVC.Common.UOW;
using ResearchMVC.Models;
using ResearchMVC.Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace ResearchMVC
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<AdventureWorksModel>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IPersonService, PersonService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}