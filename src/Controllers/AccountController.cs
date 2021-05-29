using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirusForecast.Models;
using VirusForecast.Models.AccountViewModels;
using Newtonsoft.Json;
using VirusForecast.Data.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VirusForecast.Controllers
{
    public class AccountController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IClinicRepository _clinicRepository;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;

        public AccountController(
            SignInManager<User> signInManager,
            IDoctorRepository doctorRepository,
            IClinicRepository clinicRepository,
            ILogger<AccountController> logger)
        {
            _doctorRepository = doctorRepository;
            _clinicRepository = clinicRepository;
            _signInManager = signInManager;
            _logger = logger;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            var clinics =new List<SelectListItem>();
            clinics = _clinicRepository.GetAll()
              .Select(a => new SelectListItem
              { Text = a.Name, Value = a.Id.ToString() }
              ).ToList();

            var model = new RegisterViewModel
            {
                Clinics = clinics
            };

            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var clinics = new List<SelectListItem>();
            clinics = _clinicRepository.GetAll()
              .Select(a => new SelectListItem
              { Text = a.Name, Value = a.Id.ToString() }
              ).ToList();
            model.Clinics = clinics;

            if (ModelState.IsValid)
            {
                var clinic = _clinicRepository.Get(model.ClinicId);
                var user = new User { UserName = model.Email, Email = model.Email,EmailConfirmed = false, ClinicId = model.ClinicId , Clinic = clinic};
                var errors = await _doctorRepository.Add(user, model.Password);
                if (errors == null)
                {
                    _logger.LogInformation("Doctor added");
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    AddErrors(errors);
                }
            }

            return View(model);
        }

 
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            //await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                if (!_doctorRepository.CheckIfEmailConfirmed(model.Email))
                {
                    ModelState.AddModelError(string.Empty, "Account not confirmed or doesn`t exist.");
                    return View(model);
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {

                    _logger.LogInformation("User logged in.");
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }


        #region Helpers

        private void AddErrors(IEnumerable<IdentityError> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }


        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion

    }
}
