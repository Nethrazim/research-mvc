using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearchMVC.Requests
{
    public class SearchPersonRequest
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public string searchTitle { get; set; }
        public string searchPersonType { get; set; }
        public string searchFirstName { get; set; }
        public string searchMiddleName { get; set; }
        public string searchLastName { get; set; }
        public Dictionary<string, string> search { get; set; }
    }
}