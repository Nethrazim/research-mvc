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
    public class CountryRegionDTO
    {
        public string CountryRegionCode { get; set; }

        public string Name { get; set; }
    }
}