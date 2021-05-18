using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            var fakeUserManager = new FakeUserManagerBuilder()
        .Build();
            var fakeSignInManager = new FakeSignInManagerBuilder()
                .With(x => x.Setup(sm => sm.PasswordSignInAsync(It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success))
                .Build();


            var mock = new Mock<ILogger<AccountController>>();
            ILogger<AccountController> logger = mock.Object;

            var dbContext = GetInMemoryDbContext();
            clinicRepository = new ClinicRepository(dbContext);
            doctorRepository = new DoctorRepository(dbContext, fakeUserManager.Object);
            accountController = new AccountController(fakeSignInManager.Object, doctorRepository, clinicRepository, logger);
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

            var result = await accountController.Register(newUser) as Task<ViewResult>;
            var viewResult = result.Result;
            var model = (User)viewResult.Model;
            Assert.NotNull(model);
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
            actionResultTask.Wait();
            var viewResult = actionResultTask.Result as ViewResult;
            //Assert.NotNull(viewResult);
            Assert.Equal("Home", viewResult.ViewName);
        }

        private ApplicationDbContext GetInMemoryDbContext()
        {
            DbContextOptions<ApplicationDbContext> options;
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(":testdb");
            options = builder.Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureCreated();
            dbContext.Database.EnsureDeleted();
            return dbContext;
        }
    }
}
