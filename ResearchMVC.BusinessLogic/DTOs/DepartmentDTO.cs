using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearchMVC.BusinessLogic.DTOs
{
    public class DepartmentDTO
    {
        public short DepartmentID { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}