using ResearchMVC.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResearchMVC.BusinessLogic.DTOs
{
    public class BusinessEntityContactDTO
    {
        public int BusinessEntityID { get; set; }

        public int PersonID { get; set; }

        public int ContactTypeID { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual BusinessEntityDTO BusinessEntity { get; set; }

        public virtual ContactType ContactType { get; set; }

        public virtual PersonDTO Person { get; set; }
    }
}