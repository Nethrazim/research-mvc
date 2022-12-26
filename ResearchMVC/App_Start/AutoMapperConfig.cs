using ResearchMVC.BusinessLogic.AutoMapperConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearchMVC.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());
        }
    }
}