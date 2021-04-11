using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirusForecast.Data.Interfaces;
using VirusForecast.Helpers;
using VirusForecast.Models.VirusCaseViewModel;

namespace VirusForecast.Controllers
{
    [Authorize(Roles = Models.User.DOCTOR_ROLE)]
    public class VirusController : Controller
    {

        private readonly IVirusCaseRepository _virusCaseRepository;
        private readonly IClinicRepository _clinicRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IWorkModeRepository _workModeRepository;


        private readonly ILogger<VirusController> _logger;


        public VirusController(IVirusCaseRepository virusCaseRepository, IClinicRepository clinicRepository,
            IRegionRepository regionRepository, IWorkModeRepository workModeRepository, ILogger<VirusController> logger)
        {
            _virusCaseRepository = virusCaseRepository;
            _clinicRepository = clinicRepository;
            _regionRepository = regionRepository;
            _workModeRepository = workModeRepository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            var allClinics = _clinicRepository.GetAll()
                .Select(a => new SelectListItem
                { Text = a.Name, Value = a.Id.ToString() }
                ).ToList();

            var allRegions = _regionRepository.GetAll().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToList();

            var allWorkModes = _workModeRepository.GetAll().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToList();

            return View("Add",new AddViewModel { Clinics = allClinics, Regions = allRegions, WorkdModes = allWorkModes });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddViewModel model)
        {
            var allClinics = _clinicRepository.GetAll()
               .Select(a => new SelectListItem
               { Text = a.Name, Value = a.Id.ToString() }
               ).ToList();

            var allRegions = _regionRepository.GetAll().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToList();

            var allWorkModes = _workModeRepository.GetAll().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToList();

            try
            {

                if (ModelState.IsValid)
                {

                    var virusCase = new Models.VirusCase
                    {
                        Age = model.Age,
                        Gender = model.Gender,
                        ChildrenAmount = model.ChildrenAmount,
                        VirusPositive = model.VirusPositive,
                        ClinicId = model.ClinicId,
                        RegionId = model.RegionId,
                        WorkModeId = model.WorkModeId
                    };

                    _virusCaseRepository.Add(virusCase);

                    _logger.LogInformation("Virus case added.");
                    return RedirectToAction(nameof(List));

                }
                return View(new AddViewModel { Clinics = allClinics });


            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(new AddViewModel { Clinics = allClinics, Regions = allRegions, WorkdModes = allWorkModes });
            }
        }

        public ActionResult List()
        {
            return View( new AddFromFileViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFromFile(AddFromFileViewModel model)
        {
            try
            {
                VirusCaseMap.ReadCSVFile(model.File.OpenReadStream());
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }


            return View(new AddFromFileViewModel());
        }



 

    }
}
