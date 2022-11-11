using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResearchMVC.Controllers
{
    public class BackController : Controller
    {
        // GET: Back
        public PartialViewResult Index(string link, string controller)
        {
            ViewBag.Link = link;
            ViewBag.Controller = controller;
            return PartialView();
        }
    }
}