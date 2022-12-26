using ResearchMVC.DataLayer.Repositories;
using ResearchMVC.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchMVC.BusinessLogic.UOW
{
    public interface IUnitOfWork
    {
        AdventureWorksModel dbContext { get; }
        IGenericRepository<Person> PersonRepository { get; }
        IGenericRepository<BusinessEntity> BusinessEntityRepository { get; }
        IGenericRepository<Address> AddressRepository { get; }
        IGenericRepository<AddressType> AddressTypeRepository { get; }
        IBusinessEntityContactRepository BusinessEntityContactRepository { get; }
        IGenericRepository<Employee> EmployeeRepository { get; }
        IGenericRepository<StateProvince> StateProvinceRepository { get; }
        Task Commit();
    }
}
