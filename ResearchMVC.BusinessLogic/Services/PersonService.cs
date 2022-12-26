using ResearchMVC.BusinessLogic.UOW;
using ResearchMVC.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ResearchMVC.BusinessLogic.DTOs;
using System.Threading.Tasks;
using System.Security.Policy;

namespace ResearchMVC.BusinessLogic.Services
{
    public class PersonService : IPersonService
    {
        private IUnitOfWork _uow;
        
        public PersonService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<PersonDTO> GetPersonById(int businessEntityId)
        {
            return Mapper.Map<PersonDTO>((await _uow.PersonRepository.Get<int>(p => p.BusinessEntityID == businessEntityId,null,"BusinessEntityContacts, BusinessEntityContacts.ContactType,PersonPhones, EmailAddresses")).FirstOrDefault());
        }

        public IList<Tuple<string,string>> GetCountries()
        {
            return _uow.dbContext.CountryRegions.ToList().Select(cr => new Tuple<string, string>(cr.CountryRegionCode, cr.Name)).ToList();
        }

        public IList<Tuple<int, string>> GetStateProvinces()
        {
            return _uow.dbContext.StateProvinces.ToList().Select(sp => new Tuple<int, string>(sp.StateProvinceID, sp.Name)).ToList();
        }

        public IList<Tuple<int, string>> GetAddressTypes()
        {
            return _uow.dbContext.AddressTypes.ToList().Select(at => new Tuple<int, string>(at.AddressTypeID, at.Name)).ToList();
        }

        public IList<int> GetPersonAddressTypes(int businessEntityId)
        {
            return _uow.dbContext.BusinessEntityAddresses.Include("AddressType").Where(be => be.BusinessEntityID == businessEntityId).Select(be => be.AddressType.AddressTypeID).ToList();
        }
            
        public async Task<BusinessEntityDTO> GetBusinessEntityById(int businessEntityId)
        {
            return Mapper.Map<BusinessEntityDTO>((await _uow.BusinessEntityRepository.Get<int>(p => p.BusinessEntityID == businessEntityId, null, "BusinessEntityAddresses,BusinessEntityAddresses.Address,BusinessEntityAddresses.Address.StateProvince,BusinessEntityAddresses.Address.StateProvince.CountryRegion,Person,Person.PersonPhones,Person.EmailAddresses")).FirstOrDefault());
        }

        public async Task<(IEnumerable<PersonDTO> persons, int total)> GetPersonsPaged(int skip, int take, string title, string personType, string firstName, string middleName, string lastName)
        {
            if (string.IsNullOrEmpty(title)
                && string.IsNullOrEmpty(personType)
                && string.IsNullOrEmpty(firstName)
                && string.IsNullOrEmpty(middleName)
                && string.IsNullOrEmpty(lastName)
                )
            {
                var result = await _uow.PersonRepository.GetPaged(skip, take, null, p => p.BusinessEntityID,"BusinessEntityContacts, BusinessEntityContacts.ContactType");
                return (Mapper.Map<IEnumerable<PersonDTO>>(result.entities), result.total);
            }
            else
            {
                var result = await _uow.PersonRepository.GetPaged(skip, take,
                    p => (string.IsNullOrEmpty(title) || p.Title.Contains(title)) 
                        && (string.IsNullOrEmpty(personType) || p.PersonType == personType)
                        && (string.IsNullOrEmpty(firstName) || p.FirstName == firstName) 
                        && (string.IsNullOrEmpty(middleName) || p.MiddleName == middleName)
                        && (string.IsNullOrEmpty(lastName) || p.LastName == lastName)
                    , p => p.BusinessEntityID, "BusinessEntityContacts, BusinessEntityContacts.ContactType");
                return (Mapper.Map<IEnumerable<PersonDTO>>(result.entities), result.total);
            }
        }

        public async Task<IEnumerable<string>> GetContactTypes()
        {
            var result = await _uow.BusinessEntityContactRepository.GetContactTypes();
            return result;
        }

        public async Task<bool> UpdatePerson(BusinessEntityDTO entity)
        {
            var person = entity.Person;
            var personToUpdate = (await _uow.PersonRepository.Get<int>(p => p.BusinessEntityID == person.BusinessEntityID)).FirstOrDefault();
            
            personToUpdate.ModifiedDate = DateTime.Now;
            personToUpdate.Title = person.Title;
            personToUpdate.PersonType = person.PersonType;
            personToUpdate.FirstName = person.FirstName;
            personToUpdate.MiddleName = person.MiddleName;
            personToUpdate.LastName = person.LastName;

            //Update Phones
            foreach (var phone in (person.PersonPhones ?? new List<PersonPhoneDTO>()).Where(pp => !string.IsNullOrEmpty(pp.PhoneNumber)))
            {
                var searchedPhone = personToUpdate.PersonPhones.FirstOrDefault(pp => pp.PhoneNumberTypeID == phone.PhoneNumberTypeID);

                if (searchedPhone != null && searchedPhone.PhoneNumber != phone.PhoneNumber)
                {
                    _uow.dbContext.Set<PersonPhone>().Remove(searchedPhone);
                    personToUpdate.PersonPhones.Add(new PersonPhone()
                    {
                        BusinessEntityID = personToUpdate.BusinessEntityID,
                        PhoneNumber = phone.PhoneNumber,
                        PhoneNumberTypeID = phone.PhoneNumberTypeID,
                        ModifiedDate = DateTime.Now
                    });
                }
                else if(searchedPhone == null)
                {
                    personToUpdate.PersonPhones.Add(new PersonPhone()
                    {
                        BusinessEntityID = personToUpdate.BusinessEntityID,
                        PhoneNumber = phone.PhoneNumber,
                        PhoneNumberTypeID = phone.PhoneNumberTypeID,
                        ModifiedDate = DateTime.Now
                    });
                }
            }
            //Update Email Address
            foreach (var email in (person.EmailAddresses ?? new List<EmailAddressDTO>()).Where(ed => !string.IsNullOrEmpty((ed.EmailAddress1))))
            {
                var searchedEmail = personToUpdate.EmailAddresses.FirstOrDefault();

                if (searchedEmail != null && email.EmailAddress1 != searchedEmail.EmailAddress1)
                {
                    _uow.dbContext.Set<EmailAddress>().Remove(searchedEmail);
                    personToUpdate.EmailAddresses.Add(new EmailAddress()
                    {
                        BusinessEntityID = personToUpdate.BusinessEntityID,
                        EmailAddress1 = email.EmailAddress1,
                        ModifiedDate = DateTime.Now,
                        rowguid = Guid.NewGuid()
                    });
                }
                else if(searchedEmail == null)
                {
                    personToUpdate.EmailAddresses.Add(new EmailAddress()
                    {
                        BusinessEntityID = personToUpdate.BusinessEntityID,
                        EmailAddress1 = email.EmailAddress1,
                        ModifiedDate = DateTime.Now,
                        rowguid = Guid.NewGuid()
                    });
                }
            }
            //Update Addresses
            foreach (var address in (entity.BusinessEntityAddresses ?? new List<BusinessEntityAddressDTO>()).Select(bea => bea.Address).ToList())
            {
                var addressToUpdate = _uow.dbContext.Set<Address>().Include("StateProvince").FirstOrDefault(bea => bea.AddressID == address.AddressID);
                if (addressToUpdate != null)
                {
                    addressToUpdate.AddressLine1 = address.AddressLine1;
                    addressToUpdate.AddressLine2 = address.AddressLine2;
                    addressToUpdate.City = address.City;
                    addressToUpdate.PostalCode = address.PostalCode;

                    var stateProvince = addressToUpdate.StateProvince;
                    if (stateProvince.StateProvinceID != address.StateProvince.StateProvinceID)
                    {
                        var existingStateProvince = _uow.dbContext.StateProvinces.Where(sp => sp.StateProvinceID == address.StateProvince.StateProvinceID).FirstOrDefault();
                        addressToUpdate.StateProvince = existingStateProvince;
                    }
                    _uow.AddressRepository.Update(addressToUpdate);
                }
            }

            _uow.PersonRepository.Update(personToUpdate);
            await _uow.Commit();
            return true;
        }

        public async Task<bool> CreatePerson(PersonDTO person)
        {
            person.ModifiedDate = DateTime.Now;
            person.rowguid = Guid.NewGuid();

            BusinessEntity newBusinessEntity = new BusinessEntity() { ModifiedDate = DateTime.Now, rowguid = Guid.NewGuid() };
            _uow.BusinessEntityRepository.Insert(newBusinessEntity);
            
            Person newPerson = Mapper.Map<Person>(person);
            newPerson.BusinessEntity = newBusinessEntity;

            _uow.PersonRepository.Insert(newPerson);
            await _uow.Commit();
            return true;
        }

        public async Task<bool> CreateAddress(BusinessEntityAddressDTO address)
        {
            DateTime now = DateTime.Now;
            _uow.dbContext.BusinessEntityAddresses.Add(new BusinessEntityAddress()
            {
                BusinessEntityID = address.BusinessEntityID,
                Address = new Address()
                {
                    AddressLine1 = address.Address.AddressLine1,
                    AddressLine2 = address.Address.AddressLine2,
                    City = address.Address.City,
                    PostalCode = address.Address.PostalCode,
                    StateProvinceID = address.Address.StateProvince.StateProvinceID,
                    rowguid = Guid.NewGuid(),
                    ModifiedDate = now
                },
                rowguid = Guid.NewGuid(),
                ModifiedDate = now,
                AddressTypeID = address.AddressType.AddressTypeID
            });

            await _uow.Commit();

            return true;
        }
    }
}