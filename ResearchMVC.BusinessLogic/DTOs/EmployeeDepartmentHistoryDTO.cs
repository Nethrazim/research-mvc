using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearchMVC.BusinessLogic.DTOs
{
    public class EmployeeDepartmentHistoryDTO
    {
        public int BusinessEntityID { get; set; }

        public short DepartmentID { get; set; }

        public byte ShiftID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DepartmentDTO Department { get; set; }

        public ShiftDTO Shift { get; set; }
    }
}