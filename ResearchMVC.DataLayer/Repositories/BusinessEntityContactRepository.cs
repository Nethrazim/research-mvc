using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System.Diagnostics;
using ResearchMVC.DataLayer.Models;

namespace ResearchMVC.DataLayer.Repositories
{
    public class BusinessEntityContactRepository : GenericRepository<BusinessEntityContact>, IBusinessEntityContactRepository
    {
        public BusinessEntityContactRepository(AdventureWorksModel dbContext) : base(dbContext) {}

        public async Task<List<string>> GetContactTypes()
        {
            dbContext.Database.Log = (s) => Debug.WriteLine(s);

            var result = await dbContext.People
                    .SelectMany(p => p.BusinessEntityContacts)
                    .Select(bec => bec.ContactType.Name)
                    .Distinct()
                    .ToListAsync();

            return result;
        }
    }
}