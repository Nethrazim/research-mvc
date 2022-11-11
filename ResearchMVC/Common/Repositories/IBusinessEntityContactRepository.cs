using ResearchMVC.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchMVC.Common.Repositories
{
    public interface IBusinessEntityContactRepository
    {
        Task<List<string>> GetContactTypes();
    }
}
