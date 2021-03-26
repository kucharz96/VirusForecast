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
        public ActionResult Index()
        {
            var clinics = _repository.GetAll();
            return View(clinics);
        }

        // GET: ClinicController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClinicController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClinicController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClinicController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClinicController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClinicController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClinicController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
