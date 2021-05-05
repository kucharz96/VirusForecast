using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using VirusForecast.Data.Interfaces;
using VirusForecast.Models;
using VirusForecast.Models.HomeViewModel;

namespace VirusForecast.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly IRegionRepository _regionRepository;
        private readonly IWorkModeRepository _workModeRepository;

        public HomeController(IDoctorRepository doctorRepository, 
            IRegionRepository regionRepository, IWorkModeRepository workModeRepository, ILogger<HomeController> logger)
        {
            _logger = logger;
            _doctorRepository = doctorRepository;
            _regionRepository = regionRepository;
            _workModeRepository = workModeRepository;
        }

        public IActionResult Index()
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

            var model = new HomeViewModel { Regions = allRegions, WorkdModes = allWorkModes };
            
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
