using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using ResearchMVC.Requests;
using ResearchMVC.BusinessLogic.Services;
using ResearchMVC.Helpers;

namespace ResearchMVC.Controllers
{
    public class HumanResourcesController : Controller
    {
        private IHumanResourcesService HRService;
        public HumanResourcesController(IHumanResourcesService hrService)
        {
            HRService = hrService;
        }

        // GET: HumanResources
        public ActionResult Index()
        {
            ViewBag.Info = InfoHelper.GetInfoMessage("");
            return View();
        }

        public async Task<JsonResult> GetEmployeesPaged(SearchPersonRequest request)
        {
            var result = await HRService.GetEmployeesPaged(request.start, request.length, request.searchTitle, request.searchFirstName, request.searchMiddleName, request.searchLastName);

            return Json(new
            {
                draw = request.draw,
                recordsTotal = result.total,
                recordsFiltered = result.total,
                data = result.employees
            }, JsonRequestBehavior.AllowGet);
        }
    }
}