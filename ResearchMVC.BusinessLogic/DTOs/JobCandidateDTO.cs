using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearchMVC.BusinessLogic.DTOs
{
    public class JobCandidateDTO
    {
        public int JobCandidateID { get; set; }
        public int? BusinessEntityID { get; set; }
        public string Resume { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}