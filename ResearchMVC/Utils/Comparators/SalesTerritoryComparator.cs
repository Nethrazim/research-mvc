using ResearchMVC.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearchMVC.Utils.Comparators
{
    public class SalesTerritoryComparator : EqualityComparer<SalesTerritory>
    {
        public override bool Equals(SalesTerritory x, SalesTerritory y)
        {
            return (x.Name == y.Name);
        }

        public override int GetHashCode(SalesTerritory obj)
        {
            return obj == null ? 0 : obj.TerritoryID;
        }
    }
}