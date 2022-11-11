using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearchMVC.Helpers
{
    public static class InfoHelper
    {
        public static (string,string) GetInfoMessage(string info)
        {
            switch (info)
            {
                case "info_personUpdated":
                    return ("info","Person has been updated");
                 case "info_personCreated":
                    return ("info","A new person has been created");
                case "error_addressLimitExceeded":
                    return ("error","Cannot create more addresses");
            }

            return ("info", "");
        }
    }
}