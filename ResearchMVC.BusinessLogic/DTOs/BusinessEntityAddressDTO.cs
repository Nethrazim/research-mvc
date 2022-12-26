using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ResearchMVC.DataLayer.Models;

namespace ResearchMVC.BusinessLogic.DTOs
{
    public class BusinessEntityAddressDTO
    {
        public int BusinessEntityID { get; set; }

        public int AddressID { get; set; }

        public int AddressTypeID { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual AddressDTO Address { get; set; }

        public virtual AddressTypeDTO AddressType { get; set; }
    }
}