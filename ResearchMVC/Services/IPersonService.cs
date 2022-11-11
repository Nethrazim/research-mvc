using ResearchMVC.DTOs;
using ResearchMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchMVC.Services
{
    public interface IPersonService
    {
        Task<(IEnumerable<PersonDTO> persons, int total)> GetPersonsPaged(int skip, int take, string title, string personType, string firstName, string middleName, string lastName);
        Task<IEnumerable<string>> GetContactTypes();
        Task<PersonDTO> GetPersonById(int businessEntityId);
        Task<bool> UpdatePerson(BusinessEntityDTO entity);
        Task<bool> CreatePerson(PersonDTO person);
        Task<bool> CreateAddress(BusinessEntityAddressDTO address);
        Task<BusinessEntityDTO> GetBusinessEntityById(int businessEntityId);
        IList<Tuple<string, string>> GetCountries();
        IList<Tuple<int, string>> GetStateProvinces();
        IList<Tuple<int, string>> GetAddressTypes();
        IList<int> GetPersonAddressTypes(int businessEntityId);
    }
}
