using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using ResearchMVC.DTOs;
using ResearchMVC.Services;

namespace ResearchMVC.Controllers
{
    public class AddressController : Controller
    {
        private IPersonService PersonService;
        public AddressController(IPersonService personService)
        {
            PersonService = personService;

        }
        public async Task<ActionResult> AddAddress(int? id)
        {
            var addressTypes = PersonService.GetPersonAddressTypes(id.Value);
            
            if (addressTypes.Count() == 6)
            {
                return RedirectToAction("Index", "Person", new { info = "error_addressLimitExceeded" });
            }

            ViewBag.ExistingAddressTypes = addressTypes;
            ViewBag.StateProvinces = PersonService.GetStateProvinces();
            ViewBag.AddressTypes = PersonService.GetAddressTypes();
            
            return View(new BusinessEntityAddressDTO() { BusinessEntityID = id.Value });
        }

        [HttpPost]
        public async Task<ActionResult> AddAddress(BusinessEntityAddressDTO beAddress)
        {
            if (ModelState.IsValid)
            {
                bool result = await PersonService.CreateAddress(beAddress);
                if (result)
                    return RedirectToAction("GetPersonById", "Person", new { id = beAddress.BusinessEntityID });
                else
                    return View(beAddress);
            }
            else
            {
                ViewBag.StateProvinces = PersonService.GetStateProvinces();
                ViewBag.AddressTypes = PersonService.GetAddressTypes();
                ViewBag.ExistingAddressTypes = PersonService.GetPersonAddressTypes(beAddress.BusinessEntityID);
                return View(beAddress);
            }
        }
    }
}