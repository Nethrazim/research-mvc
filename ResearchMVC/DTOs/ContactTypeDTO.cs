using ResearchMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResearchMVC.DTOs
{
    public class ContactTypeDTO
    {
        public int ContactTypeID { get; set; }

        public string Name { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}