using ResearchMVC.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq.Expressions;

namespace ResearchMVC.DataLayer.Repositories
{
    public class EmployeesRepository : GenericRepository<Employee>, IEmployeesRepository
    {
        public EmployeesRepository(AdventureWorksModel dbContext) : base(dbContext) {}
    }
}