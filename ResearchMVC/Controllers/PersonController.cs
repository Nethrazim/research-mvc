using ResearchMVC.BusinessLogic.DTOs;
using ResearchMVC.Helpers;
using ResearchMVC.Requests;
using ResearchMVC.BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using Unity;

namespace ResearchMVC.Controllers
{
    public class PersonController : Controller
    {
        private IPersonService PersonService;
        public PersonController(IPersonService personService)
        {
            PersonService = personService;
        }

        /* ALL PERSONS */
        // GET: Person
        public async Task<ActionResult> Index(string info)
        {
            ViewBag.Info = InfoHelper.GetInfoMessage(info);
            return View();
        }

        
        public async Task<JsonResult> GetPersonsPaged(SearchPersonRequest request)
        {
            var result = await PersonService.GetPersonsPaged(request.start, request.length, request.searchTitle, request.searchPersonType == "all" ? null : request.searchPersonType, request.searchFirstName, request.searchMiddleName, request.searchLastName);

            return Json(new { 
                draw = request.draw,
                recordsTotal = result.total,
                recordsFiltered =  result.total,
                data = result.persons
            }, JsonRequestBehavior.AllowGet);
        }

        /*EDIT PERSON*/
        public async Task<ActionResult> GetPersonById(int? id)
        {
            var person = await PersonService.GetBusinessEntityById(id.Value);
            ViewBag.Countries = PersonService.GetCountries();
            ViewBag.StateProvinces = PersonService.GetStateProvinces();
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdatePerson(BusinessEntityDTO entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = await PersonService.UpdatePerson(entity);
                    return RedirectToAction("Index", new { info = "info_personUpdated" });
                }
                else
                {
                    ViewBag.Countries = PersonService.GetCountries();
                    return View("GetPersonById", entity);
                }
            }
            catch (DbUpdateException ex)
            {
                SqlException innerException = ex.InnerException.InnerException as SqlException;
                if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
                {
                    ViewBag.Countries = PersonService.GetCountries();
                    return View("GetPersonById", entity);
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Countries = PersonService.GetCountries();
                ModelState.AddModelError("Internal Server Error", "An Internal Server Error occured.");
                return RedirectToAction("GetPersonById", new { id = entity.BusinessEntityID });
            }
        }
        /*CREATE PERSON*/

        [HttpGet]
        public async Task<ActionResult> CreatePerson()
        {
            return View(new PersonDTO());
        }

        [HttpPost]
        public async Task<ActionResult> CreatePerson(PersonDTO person)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await PersonService.CreatePerson(person);
                    return RedirectToAction("Index", new { info = "info_personCreated" });
                }
                else
                {
                    return View("CreatePerson", person);
                }
            }
            catch (Exception ex)
            {
                return View("CreatePerson", person);
            }
        }
    }
}