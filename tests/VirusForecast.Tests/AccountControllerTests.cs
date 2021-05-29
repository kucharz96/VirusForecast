using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirusForecast.Controllers;
using VirusForecast.Data;
using VirusForecast.Data.Interfaces;
using VirusForecast.Models;
using VirusForecast.Models.AccountViewModels;
using Xunit;

namespace VirusForecast.Tests
{
    public class AccountControllerTests
    {
        private readonly ILogger logger;
        private readonly AccountController accountController;
        private readonly IDoctorRepository doctorRepository;
        private readonly IClinicRepository clinicRepository;

        public AccountControllerTests()
        {
            MemoryApplicationContext context = new MemoryApplicationContext();
            doctorRepository = new Mock<DoctorRepository>(context.Context, context.UserManager).Object;
            clinicRepository = new Mock<ClinicRepository>(context.Context).Object;
            var mock = new Mock<ILogger<AccountController>>();

            ILogger<AccountController> logger = mock.Object;

            accountController = new AccountController(context.SignInManager, doctorRepository, clinicRepository, logger);



        }

        [Fact]
        public void Register()
        {
            var result = accountController.Register();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Register_PostValidExecutedAsync()
        {
            var newUser = new RegisterViewModel
            {
                ClinicId = "7BF81445-3F14-41B8-9F4D-A051A83D0BA4",
                Clinics = null,
                Email = "mati@gmail.com",
                Password = "12345678A.",
                ConfirmPassword = "12345678A."
            };

            var result = await accountController.Register(newUser);
            var user = this.doctorRepository.GetByEmail(newUser.Email);
            Assert.NotNull(result);
            Assert.NotNull(user);
        }

        [Fact]
        public async Task Login()
        {
            var result = await accountController.Login() as ViewResult;
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void LogOut()
        {
            var actionResultTask = accountController.Logout();
            Assert.IsAssignableFrom<Task<IActionResult>>(actionResultTask);
        }
    }
}
