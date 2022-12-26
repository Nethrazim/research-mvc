using AutoMapper;
using ResearchMVC.BusinessLogic.UOW;
using ResearchMVC.BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ResearchMVC.BusinessLogic.Services
{
    public class HumanResourcesService : IHumanResourcesService
    {
        private IUnitOfWork _uow;

        public HumanResourcesService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<(IEnumerable<EmployeeDTO> employees, int total)> GetEmployeesPaged(int skip, int take, string title, string firstName, string middleName, string lastName)
        {
            if (string.IsNullOrEmpty(title)
                && string.IsNullOrEmpty(firstName)
                && string.IsNullOrEmpty(middleName)
                && string.IsNullOrEmpty(lastName)
                )
            {
                var result = await _uow.EmployeeRepository.GetPaged(skip, take, null, p => p.BusinessEntityID, "Person,EmployeeDepartmentHistories");
                return (Mapper.Map<IEnumerable<EmployeeDTO>>(result.entities), result.total);
            }
            else
            {
                var result = await _uow.EmployeeRepository.GetPaged(skip, take,
                    p => (string.IsNullOrEmpty(title) || p.Person.Title.Contains(title))
                        && (string.IsNullOrEmpty(firstName) || p.Person.FirstName == firstName)
                        && (string.IsNullOrEmpty(middleName) || p.Person.MiddleName == middleName)
                        && (string.IsNullOrEmpty(lastName) || p.Person.LastName == lastName)
                    , p => p.BusinessEntityID, "Person");
                return (Mapper.Map<IEnumerable<EmployeeDTO>>(result.entities), result.total);
            }
        }

    }

}