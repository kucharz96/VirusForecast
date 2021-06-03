using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using VirusForecast.Data.Interfaces;
using VirusForecast.Models;
using VirusForecast.Models.AccountViewModels;
using VirusForecast.Models.DoctorViewModel;

namespace VirusForecast.Controllers
{
    [Authorize(Roles = Models.User.ADMIN_ROLE)]
    public class DoctorController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IClinicRepository _clinicRepository;

        private readonly ILogger<DoctorController> _logger;


        public DoctorController(IDoctorRepository doctorRepository,
            IClinicRepository clinicRepository,
            ILogger<DoctorController> logger)
        {
            _doctorRepository = doctorRepository;
            _clinicRepository = clinicRepository;
            _logger = logger;
        }

        // GET: DoctorController
        public ActionResult List()
        {
            IEnumerable<User> doctors = _doctorRepository.GetAll();
            var list = doctors.Select(x => new DoctorListViewModel()
            {
                ClinicId = x.ClinicId,
                Clinic = x.Clinic,
                Email = x.Email,
                EmailConfirmed = x.EmailConfirmed,
                EmailConfirmedString = x.EmailConfirmed ? "Yes" : "No",
                Id = x.Id
            }); 
            return View(list);
        }


        // GET: DoctorController/Create
        public ActionResult Add()
        {
            var allClinics = _clinicRepository.GetAll()
                .Select(a=>new SelectListItem 
                { Text = a.Name, Value = a.Id.ToString()}
                ).ToList();
            return View(new AddEditViewModel {Clinics = allClinics});
        }

        // POST: DoctorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddEditViewModel model)
        {
            var allClinics = _clinicRepository.GetAll()
             .Select(a => new SelectListItem
             { Text = a.Name, Value = a.Id.ToString() }
             ).ToList();
            try
            {
 
                if (ModelState.IsValid)
                {

                    var user = new Models.User 
                    { 
                        UserName = model.Email, 
                        Email = model.Email, 
                        EmailConfirmed = model.EmailConfirmed,
                        ClinicId = model.ClinicId,

                    };
                    var errors = await _doctorRepository.Add(user, model.Password);
                    if (errors == null)
                    {
                        _logger.LogInformation("Doctor added");
                        return RedirectToAction(nameof(List));
                    }
                    else
                    {
                        AddErrors(errors);
                    }

                }
                return View(new AddEditViewModel { Clinics = allClinics });


            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(new AddEditViewModel { Clinics = allClinics });
            }
        }

        // GET: DoctorController/Edit/5
        public ActionResult Edit(string id)
        {
            var allClinics = _clinicRepository.GetAll()
                .Select(a => new SelectListItem
                { Text = a.Name, Value = a.Id.ToString() }
                ).ToList();

            var doctor = _doctorRepository.Get(id);

            var model = new EditViewModel
            {
                Clinics = allClinics,
                Id = doctor.Id,
                Email = doctor.Email,
                EmailConfirmed = doctor.EmailConfirmed,
                ClinicId = doctor.ClinicId,
                //Password = doctor.PasswordHash,
            };
            return View(model);
        }

        // POST: DoctorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            var allClinics = _clinicRepository.GetAll()
            .Select(a => new SelectListItem
            { Text = a.Name, Value = a.Id.ToString() }
            ).ToList();
            model.Clinics = allClinics;
            try
            {

                if (ModelState.IsValid)
                {
                    _doctorRepository.Edit(model);
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

        // POST: DoctorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            try
            {
                _doctorRepository.Delete(id);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return RedirectToAction(nameof(List));
            }
        }

        private void AddErrors(IEnumerable<IdentityError> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
