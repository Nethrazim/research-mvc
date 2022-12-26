using AutoMapper;
using ResearchMVC.BusinessLogic.UOW;
using ResearchMVC.BusinessLogic.DTOs;
using ResearchMVC.DataLayer.Models;
using ResearchMVC.BusinessLogic.Services;
using ResearchMVC.Utils.Comparators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ResearchMVC.Controllers
{
    public class StateProvinceController : Controller
    {
        private IUnitOfWork uow;
        private IPersonService PersonService;
        public StateProvinceController(IUnitOfWork uow, IPersonService personService)
        {
            PersonService = personService;
            this.uow = uow;
        }

        // GET: StateProvince
        public ActionResult CreateStateProvince()
        {
            ViewBag.Countries = Mapper.Map<List<CountryRegionDTO>>(uow.dbContext.CountryRegions.ToList());
            ViewBag.Teritories = uow.dbContext.SalesTerritories.GroupBy(g => g.Name).ToList().Select(g => new SalesTerritory() { Name = g.Key, TerritoryID = g.FirstOrDefault().TerritoryID }).ToList();
            return View(new StateProvinceDTO());
        }

        [HttpPost]
        public async Task<ActionResult> Create(StateProvinceDTO entity)
        {
            try
            {
                entity.rowguid = Guid.NewGuid();
                entity.ModifiedDate = DateTime.Now;
                uow.StateProvinceRepository.Insert(Mapper.Map<StateProvince>(entity));
                await uow.Commit();
                return RedirectToAction("CreateStateProvince");
            }
            catch (Exception ex)
            {
                return View("CreateStateProvince", entity);
            }
        }
    }
}