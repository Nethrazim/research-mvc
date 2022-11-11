using ResearchMVC.Common.Repositories;
using ResearchMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchMVC.Common.UOW
{
    public interface IUnitOfWork
    {
        AdventureWorksModel dbContext { get; }
        IGenericRepository<Person> PersonRepository { get; }
        IGenericRepository<BusinessEntity> BusinessEntityRepository { get; }
        IGenericRepository<Address> AddressRepository { get; }
        IGenericRepository<AddressType> AddressTypeRepository { get; }
        IBusinessEntityContactRepository BusinessEntityContactRepository { get; }

        Task Commit();
    }
}
