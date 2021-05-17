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
using VirusForecast.Models.DoctorViewModel;
using Xunit;

namespace VirusForecast.Tests
{
    public class DoctorControllerTests
    {
        private readonly ILogger<DoctorController> logger;
        private readonly DoctorController doctorController;
        private readonly IDoctorRepository doctorRepository;
        private readonly UserManager<User> userManager;
        
        private readonly IClinicRepository clinicRepository;

        public DoctorControllerTests()
        {
            logger = null;
            var dbContext = GetInMemoryDbContext();
            doctorRepository = new DoctorRepository(dbContext, userManager);
            doctorController = new DoctorController(doctorRepository,clinicRepository, logger);
        }

        [Fact]
        public void List()
        {
            var result = doctorController.List();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void List_CheckIfNotEmpty()
        {
            var result = doctorController.List();
            var viewResult = Assert.IsType<ViewResult>(result);
            var doctors = Assert.IsAssignableFrom<IEnumerable<DoctorListViewModel>>(viewResult.Model);
            Assert.NotNull(doctors);
        }
        
        [Fact]
        public void Add()
        {
            var newDoctor = new AddEditViewModel
            {
                Email = "mati@gmail.com",
                EmailConfirmed = true,
                Password = "12345678A.",
                ConfirmPassword = "12345678A."
            };

            var result = doctorController.Add(newDoctor);
            var viewResult = Assert.IsType<ViewResult>(result);
            var testDoctor = Assert.IsType<User>(viewResult.Model);
            Assert.Equal(newDoctor.Email, testDoctor.Email);
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
