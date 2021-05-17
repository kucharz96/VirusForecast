using Microsoft.AspNetCore.Identity;
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
using VirusForecast.Models.VirusCaseViewModel;
using Xunit;

namespace VirusForecast.Tests
{
    public class VirusControllerTests
    {
        private readonly IVirusCaseRepository virusCaseRepository;
        private readonly IClinicRepository clinicRepository;
        private readonly IRegionRepository regionRepository;
        private readonly IWorkModeRepository workModeRepository;
        private readonly UserManager<User> userManager;
        private readonly VirusController virusController;
        private readonly ILogger<VirusController> logger;

        public VirusControllerTests()
        {
            logger = null;
            var dbContext = GetInMemoryDbContext();
            virusCaseRepository = new VirusCaseRepository(dbContext);
            clinicRepository = new ClinicRepository(dbContext);
            regionRepository = new RegionRepository(dbContext);
            workModeRepository = new WorkModeRepository(dbContext);
            virusController = new VirusController(virusCaseRepository, clinicRepository, regionRepository, workModeRepository, logger, userManager);
        }

        [Fact]
        public void Index()
        {
            var result = virusController.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void List_CheckIfNotEmpty()
        {
            var result = virusController.List();
            var viewResult = Assert.IsType<ViewResult>(result);
            var virusCases = Assert.IsAssignableFrom<IEnumerable<VirusCaseListViewModel>>(viewResult.Model);
            Assert.NotNull(virusCases);
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
