using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ResearchMVC.DataLayer.Models;
using System.Data.Entity.Spatial;

namespace ResearchMVC.BusinessLogic.DTOs
{
    public class AddressTypeDTO
    {
        public int AddressTypeID { get; set; }
        public string Name { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}