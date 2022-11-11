using ResearchMVC.Common.Repositories;
using ResearchMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ResearchMVC.Common.UOW
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
    }
}