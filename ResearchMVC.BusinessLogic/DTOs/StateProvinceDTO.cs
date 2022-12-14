using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ResearchMVC.DataLayer.Models;
using System.Data.Entity.Spatial;

namespace ResearchMVC.BusinessLogic.DTOs
{
    public class StateProvinceDTO
    {
        public int StateProvinceID { get; set; }
        public string StateProvinceCode { get; set; }
        public string CountryRegionCode { get; set; }
        public bool IsOnlyStateProvinceFlag { get; set; }
        public string Name { get; set; }
        public int TerritoryID { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
        public CountryRegionDTO CountryRegion { get; set; }
    }
}