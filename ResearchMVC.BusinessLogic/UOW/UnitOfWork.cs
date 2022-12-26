using ResearchMVC.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ResearchMVC.DataLayer.Repositories;

namespace ResearchMVC.BusinessLogic.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private AdventureWorksModel _dbContext;
        public AdventureWorksModel dbContext => _dbContext;

        private IGenericRepository<Person> _personRepository;
        private IGenericRepository<BusinessEntity> _businessEntityRepository;
        private IGenericRepository<Address> _addressRepository;
        private IGenericRepository<AddressType> _addressTypeRepository;
        private IBusinessEntityContactRepository _businessEntityContactRepository;
        private IGenericRepository<Employee> _employeeRepository;
        private IGenericRepository<StateProvince> _stateProvinceRepository;
        public UnitOfWork(AdventureWorksModel dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IGenericRepository<Person> PersonRepository
        {
            get
            {
                return _personRepository ?? (_personRepository = new GenericRepository<Person>(_dbContext));
            }
        }

        public IGenericRepository<BusinessEntity> BusinessEntityRepository
        {
            get
            {
                return _businessEntityRepository ?? (_businessEntityRepository = new GenericRepository<BusinessEntity>(_dbContext));
            }
        }

        public IGenericRepository<Address> AddressRepository
        {
            get
            {
                return _addressRepository ?? (_addressRepository = new GenericRepository<Address>(_dbContext));
            }
        }

        public IGenericRepository<AddressType> AddressTypeRepository
        {
            get
            {
                return _addressTypeRepository ?? (_addressTypeRepository = new GenericRepository<AddressType>(_dbContext));
            }
        }

        public IBusinessEntityContactRepository BusinessEntityContactRepository
        {
            get
            {
                return _businessEntityContactRepository ?? (_businessEntityContactRepository = new BusinessEntityContactRepository(_dbContext));
            }
        }

        public IGenericRepository<Employee> EmployeeRepository
        {
            get
            {
                return _employeeRepository ?? (_employeeRepository = new GenericRepository<Employee>(_dbContext));
            }
        }

        public IGenericRepository<StateProvince> StateProvinceRepository
        {
            get
            {
                return _stateProvinceRepository ?? (_stateProvinceRepository = new GenericRepository<StateProvince>(_dbContext));
            }
        }
    }
}