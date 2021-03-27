using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirusForecast.Data;
using VirusForecast.Data.Interfaces;

namespace VirusForecast.Controllers
{
    [Authorize(Roles = Models.User.ADMIN_ROLE)]
    public class ClinicController : Controller
    {
        private readonly ILogger<ClinicController> _logger;
        private readonly IClinicRepository _repository;


        public ClinicController( ILogger<ClinicController> logger, IClinicRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: ClinicController
        public ActionResult List()
        {
            var clinics = _repository.GetAll();
            return View(clinics);
        }

        // GET: ClinicController/Details/5
        public ActionResult Details(string id)
        {
            var details =_repository.Get(id);
            return View(details);
        }

        // GET: ClinicController/Details/name
        //public ActionResult Details(string name)
        //{
        //    var details = _repository.Get(name);
        //    return View(details);
        //}

        // GET: ClinicController/Add
        public ActionResult Add(string name)
        {
            return View();
        }

        // POST: ClinicController/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(IFormCollection collection)
        {
            try
            {
                _repository.Add(collection["Name"]);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClinicController/Edit/5
        public ActionResult Edit(string id)
        {
            return View();
        }

        // POST: ClinicController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                _repository.Edit(id, collection["Name"]);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClinicController/Delete/5
        public ActionResult Delete(string id)
        {
            var deleted_clinic = _repository.Delete(id);
            return View(deleted_clinic);
        }
    }
}
