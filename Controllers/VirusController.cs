using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VirusForecast.Data.Interfaces;
using VirusForecast.Helpers;
using VirusForecast.Models;
using VirusForecast.Models.VirusCaseViewModel;

namespace VirusForecast.Controllers
{

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
        [Authorize(Roles = Models.User.DOCTOR_ROLE + "," + Models.User.ADMIN_ROLE)]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = Models.User.DOCTOR_ROLE + "," + Models.User.ADMIN_ROLE)]
        public ActionResult Add()
        {
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
            var allClinics = _clinicRepository.GetAll()
                .Select(a => new SelectListItem
                { Text = a.Name, Value = a.Id.ToString() }
                ).ToList();
            var model = new AddViewModel { Clinics = allClinics, Regions = allRegions, WorkdModes = allWorkModes };

            if (User.IsInRole(Models.User.DOCTOR_ROLE))
            {
                var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var clinicId = _clinicRepository.GetDoctorsClinics(doctorId).FirstOrDefault().Id;

                model.ClinicId = clinicId;

            }
            return View("Add", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Models.User.DOCTOR_ROLE + "," + Models.User.ADMIN_ROLE)]
        public IActionResult Add(AddViewModel model)
        {
            var allClinics = _clinicRepository.GetAll()
               .Select(a => new SelectListItem
               { Text = a.Name, Value = a.Id.ToString() }
               ).ToList();

            if (User.IsInRole(Models.User.DOCTOR_ROLE))
            {
                var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var clinicId = _clinicRepository.GetDoctorsClinics(doctorId).FirstOrDefault().Id;

                model.ClinicId = clinicId;
            }


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
                        Date = model.Date,
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

        [Authorize(Roles = Models.User.DOCTOR_ROLE + "," + Models.User.ADMIN_ROLE)]
        public IActionResult List()
        {
            IEnumerable<VirusCase> virusCases;
            if (User.IsInRole(Models.User.DOCTOR_ROLE))
            {
                var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                virusCases = _virusCaseRepository.GetDoctorsVirusCases(doctorId);

            }
            else
            {
                virusCases = _virusCaseRepository.GetAll();
            }
            var list = virusCases.Select(x => new VirusCaseListViewModel()
            {
                Id = x.Id,
                Age = x.Age,
                ChildrenAmount = x.ChildrenAmount,
                ClinicId = _clinicRepository.GetClinicName(x.ClinicId),
                RegionId = _regionRepository.GetName(x.RegionId),
                Gender = x.Gender,
                Date = x.Date,
                DateString = x.Date.ToString("yyyy-MM-dd"),
                VirusPositive = x.VirusPositive,
                VirusPositiveString = x.VirusPositive ? "Yes" : "No",
                WorkModeId = _workModeRepository.GetName(x.WorkModeId)
            });


            return View(list);
        }

        [Authorize(Roles = Models.User.DOCTOR_ROLE + "," + Models.User.ADMIN_ROLE)]
        public IActionResult AddFromFile()
        {
            var model = new AddFromFileViewModel();


            var allClinics = _clinicRepository.GetAll()
               .Select(a => new SelectListItem
               { Text = a.Name, Value = a.Id.ToString() }
               ).ToList();

            model.Clinics = allClinics;
            if (User.IsInRole(Models.User.DOCTOR_ROLE))
            {
                var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var clinicId = _clinicRepository.GetDoctorsClinics(doctorId).FirstOrDefault().Id;

                model.ClinicId = clinicId;
            }



            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Models.User.DOCTOR_ROLE + "," + Models.User.ADMIN_ROLE)]
        public IActionResult AddFromFile(AddFromFileViewModel model)
        {
            try
            {
                var cases = VirusCaseMap.ReadCSVFile(model.File.OpenReadStream());



                foreach (var item in cases)
                {
                    item.Id = Guid.NewGuid().ToString();
                    var region = _regionRepository.GetByName(item.Region.Name);
                    if (region == null)
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

                    if (User.IsInRole(Models.User.DOCTOR_ROLE))
                    {
                        var user = _userManager.GetUserAsync(User).Result;

                        if (string.IsNullOrEmpty(user.ClinicId))
                        {
                            throw new Exception($"Current doctor have no clinic");
                        }
                        else
                        {
                            item.ClinicId = user.ClinicId;
                        }
                    }
                    else
                    {
                        item.ClinicId = model.ClinicId;
                    }

                    item.WorkMode = mode;
                    item.Region = region;
                    _virusCaseRepository.Add(item);


                    _logger.LogInformation("Virus case added.");

                }
                return RedirectToAction(nameof(List));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }




            return RedirectToAction("AddFromFile");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Models.User.DOCTOR_ROLE + "," + Models.User.ADMIN_ROLE)]
        public ActionResult Delete(string id)
        {
            try
            {
                _virusCaseRepository.Delete(id);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return RedirectToAction(nameof(List));
            }
        }

        [Authorize(Roles = Models.User.DOCTOR_ROLE + "," + Models.User.ADMIN_ROLE)]
        public ActionResult Edit(string id)
        {
            var user = _userManager.GetUserAsync(User).Result;


            var allClinics = new List<SelectListItem>();
            var allRegions = new List<SelectListItem>();
            var allWorkModes = new List<SelectListItem>();

            if (_userManager.IsInRoleAsync(user, "ADMIN").Result)
            {
                allClinics = _clinicRepository.GetAll()
               .Select(a => new SelectListItem
               { Text = a.Name, Value = a.Id.ToString() }
               ).ToList();
            }

            if (_userManager.IsInRoleAsync(user, "DOCTOR").Result)
            {
                allClinics = _clinicRepository.GetDoctorsClinics(user.Id)
               .Select(a => new SelectListItem
               { Text = a.Name, Value = a.Id.ToString() }
               ).ToList();
            }

            allRegions = _regionRepository.GetAll().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToList();

            allWorkModes = _workModeRepository.GetAll().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToList();

            var virusCase = _virusCaseRepository.Get(id);

            var model = new AddViewModel
            {
                Id = virusCase.Id,
                Age = virusCase.Age,
                ChildrenAmount = virusCase.ChildrenAmount,
                Gender = virusCase.Gender,
                VirusPositive = virusCase.VirusPositive,
                Date = virusCase.Date,
                ClinicId = virusCase.ClinicId,
                RegionId = virusCase.RegionId,
                WorkModeId = virusCase.WorkModeId,
                Clinics = allClinics,
                Regions = allRegions,
                WorkdModes = allWorkModes
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Models.User.DOCTOR_ROLE + "," + Models.User.ADMIN_ROLE)]
        public ActionResult Edit(AddViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _virusCaseRepository.Edit(model);
                    return RedirectToAction(nameof(List));


                }
                return View(model);


            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        [AllowAnonymous]
        public JsonResult GetCasesStatistics(string dateFrom, string dateTo)
        {
            CaseStatisticFilters filters = new CaseStatisticFilters();
            try
            {
                filters.DateFrom = DateTime.Parse(dateFrom);
                filters.DateTo = DateTime.Parse(dateTo);
            }
            catch (Exception)
            {

            }

            var realCases = _virusCaseRepository.GetRealCases(filters);
            var forecastCases = _virusCaseRepository.GetForecastCases(filters);

            var totalCases = new TotalCaseStatisic
            {
                RealCases = realCases,
                ForecastCases = forecastCases
            };

            return Json(totalCases);
        }

    }
}
