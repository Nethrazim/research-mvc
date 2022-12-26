using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ResearchMVC.DataLayer.Models
{
    public partial class AdventureWorksModel : DbContext
    {
        public AdventureWorksModel(string connectionString)
            : base(connectionString)
        {
        }
    }
}
