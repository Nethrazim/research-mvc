using ResearchMVC.BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchMVC.BusinessLogic.Services
{
    public interface IHumanResourcesService
    {
        Task<(IEnumerable<EmployeeDTO> employees, int total)> GetEmployeesPaged(int skip, int take, string title, string firstName, string middleName, string lastName);
    }
}
