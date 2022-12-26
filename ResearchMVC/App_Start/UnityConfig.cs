using ResearchMVC.BusinessLogic.Services;
using ResearchMVC.BusinessLogic.UOW;
using ResearchMVC.DataLayer.Repositories;
using ResearchMVC.DataLayer.Models;
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

            //Repositories
            container.RegisterType<IEmployeesRepository, EmployeesRepository>();

            //Services
            container.RegisterType<IPersonService, PersonService>();
            container.RegisterType<IHumanResourcesService, HumanResourcesService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}