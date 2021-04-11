using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        private readonly ILogger<VirusController> _logger;


        public VirusController(IVirusCaseRepository virusCaseRepository,ILogger<VirusController> logger)
        {
            _virusCaseRepository = virusCaseRepository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddFromFile()
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
