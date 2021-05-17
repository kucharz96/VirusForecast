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
using Xunit;

namespace VirusForecast.Tests
{
    public class HomeControllerTests
    {
        private readonly IDoctorRepository doctorRepository;
        private readonly ILogger<HomeController> logger;
        private readonly IRegionRepository regionRepository;
        private readonly IWorkModeRepository workModeRepository;
        private readonly UserManager<User> userManager;
        private readonly HomeController homeController;

        public HomeControllerTests()
        {
            logger = null;
            var dbContext = GetInMemoryDbContext();
            doctorRepository = new DoctorRepository(dbContext, userManager);
            regionRepository = new RegionRepository(dbContext);
            workModeRepository = new WorkModeRepository(dbContext);
            homeController = new HomeController(doctorRepository, regionRepository, workModeRepository, logger);
        }

        [Fact]
        public void Index()
        {
            var result = homeController.Index();
            Assert.IsType<ViewResult>(result);
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
