﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirusForecast.Data.Interfaces;
using VirusForecast.Helpers;
using VirusForecast.Models;
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
        private readonly UserManager<User> _userManager;



        private readonly ILogger<VirusController> _logger;


        public VirusController(IVirusCaseRepository virusCaseRepository, IClinicRepository clinicRepository,
            IRegionRepository regionRepository, IWorkModeRepository workModeRepository, ILogger<VirusController> logger,
            UserManager<User> userManager)
        {
            _virusCaseRepository = virusCaseRepository;
            _clinicRepository = clinicRepository;
            _regionRepository = regionRepository;
            _workModeRepository = workModeRepository;
            _logger = logger;
            _userManager = userManager;
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

        public IActionResult List()
        {
            return View();
        }

        public IActionResult AddFromFile()
        {
            return View(new AddFromFileViewModel());
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFromFile(AddFromFileViewModel model)
        {
            try
            {
                var cases = VirusCaseMap.ReadCSVFile(model.File.OpenReadStream());



                foreach(var item in cases)
                {
                    item.Id = Guid.NewGuid().ToString();
                    var region = _regionRepository.GetByName(item.Region.Name);
                    if(region == null)
                    {
                        throw new Exception($"Region {item.Region.Name} not exist");
                    }
                    else
                    {
                        item.RegionId = region.Id;
                    }

                    var mode = _workModeRepository.GetByName(item.WorkMode.Name);


                    if (mode == null)
                    {
                        throw new Exception($"Work mode {item.WorkMode.Name} not exist");
                    }
                    else
                    {
                        item.WorkModeId = mode.Id;
                    }

                    var user = _userManager.GetUserAsync(User).Result;

                    if (string.IsNullOrEmpty(user.ClinicId))
                    {
                        throw new Exception($"Current doctor have no clinic");
                    }
                    else
                    {
                        item.ClinicId = user.ClinicId;
                    }

                    _virusCaseRepository.Add(item);


                    _logger.LogInformation("Virus case added.");

                }



            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }


            return View(new AddFromFileViewModel());
        }



 

    }
}
