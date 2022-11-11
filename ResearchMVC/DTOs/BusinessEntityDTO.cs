using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ResearchMVC.Models;

namespace ResearchMVC.DTOs
{
    public class BusinessEntityDTO
    {
        public int BusinessEntityID { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
        public  ICollection<BusinessEntityAddressDTO> BusinessEntityAddresses { get; set; }
        //public  ICollection<BusinessEntityContactDTO> BusinessEntityContacts { get; set; }
        public  PersonDTO Person { get; set; }
    }
}