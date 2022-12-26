using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearchMVC.BusinessLogic.DTOs
{
    public class ShiftDTO
    {
        public byte ShiftID { get; set; }

        public string Name { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}