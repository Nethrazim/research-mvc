using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ResearchMVC.Models;

namespace ResearchMVC.DTOs
{
    public class PersonDTO
    {
        public int BusinessEntityID { get; set; }
        [Required]
        public string PersonType { get; set; }
        
        public bool NameStyle { get; set; }
        
        public string Title { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string Suffix { get; set; }

        public int EmailPromotion { get; set; }

        public string AdditionalContactInfo { get; set; }

        public string Demographics { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ContactTypeDescription => ContactType?.Name;
        public ContactTypeDTO ContactType { get; set; }

        public ICollection<PersonPhoneDTO> PersonPhones { get; set; }
        public ICollection<EmailAddressDTO> EmailAddresses { get; set; }
        public bool IsContact => PersonType.Contains("C");
    }
}