using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearchMVC.BusinessLogic.DTOs
{
    public class EmployeePayHistoryDTO
    {
        public int BusinessEntityID { get; set; }
        public DateTime RateChangeDate { get; set; }
        public decimal Rate { get; set; }
        public byte PayFrequency { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}