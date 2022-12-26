using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ResearchMVC.DataLayer.Models;
using System.Data.Entity.Spatial;

namespace ResearchMVC.BusinessLogic.DTOs
{
    public class AddressDTO
    {
        public int AddressID { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public int StateProvinceID { get; set; }

        public string PostalCode { get; set; }

        public DbGeography SpatialLocation { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
        public StateProvinceDTO StateProvince { get; set; }
    }
}