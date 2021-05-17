using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirusForecast.Controllers;
using VirusForecast.Data;
using VirusForecast.Data.Interfaces;
using VirusForecast.Models;
using Xunit;

namespace VirusForecast.Tests
{
    public class ClinicControllerTests
    {
        private readonly ILogger<ClinicController>  logger;
        private readonly ClinicController clinicController;
        private readonly IClinicRepository clinicRepository;

        public ClinicControllerTests()
        {
            logger = null;
            var dbContext = GetInMemoryDbContext();
            clinicRepository = new ClinicRepository(dbContext);
            clinicController = new ClinicController(logger, clinicRepository);
        }

        [Fact]
        public void List()
        {
            var result = clinicController.List();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void List_CheckIfNotEmpty()
        {
            var result = clinicController.List();
            var viewResult = Assert.IsType<ViewResult>(result);
            var clinics = Assert.IsAssignableFrom<IEnumerable<Clinic>>(viewResult.Model);
            Assert.NotNull(clinics);
        }

        [Fact]
        public void Create()
        {
            var result = clinicController.Add();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_ValidModelState()
        {
            clinicController.ModelState.AddModelError("name", "Name is required!");
            var name = "Health Clinic";
            var result = clinicController.Add(name);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            var action = Assert.IsType<String>(viewResult.ActionName);
            Assert.Equal("List", action.ToString());
        }

        [Fact]
        public void Delete_CompareElements()
        {
            var name = "Health Clinic";
            clinicController.Add(name);
            var nameTwo = "Healthy Clinic";
            clinicController.Add(nameTwo);

            var result = clinicController.List();
            var viewResult = Assert.IsType<ViewResult>(result);
            var clinics = Assert.IsAssignableFrom<IEnumerable<Clinic>>(viewResult.Model);
            var clinicCounts = clinics.Count();

            var clinicId = clinics.First().Id;
            clinicController.Delete(clinicId);

            var resultAfter = clinicController.List();
            var viewResultAfter = Assert.IsType<ViewResult>(resultAfter);
            var clinicsAfter = Assert.IsAssignableFrom<IEnumerable<Clinic>>(viewResultAfter.Model);
            var clinicCountsAfter = clinics.Count();

            Assert.NotEqual(0, clinicCounts);

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
