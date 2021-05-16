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
using VirusForecast.Models.ClinicViewModel;

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
        public ActionResult Add()
        {
            return View();
        }

        // POST: ClinicController/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(string name)
        {
            try
            {
                _repository.Add(name);
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
            var clinic = _repository.Get(id);
            var model = new AddEditViewModel
            {
                Id = clinic.Id,
                Name = clinic.Name
            };
            return View(model);
        }

        // POST: ClinicController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, string name)
        {
            try
            {
                _repository.Edit(id, name);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClinicController/Delete/5
        [HttpPost]
        public ActionResult Delete(string id)
        {
            _repository.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}
