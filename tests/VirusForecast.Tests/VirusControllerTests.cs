using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly VirusController virusController;

        public VirusControllerTests()
        {
            MemoryApplicationContext context = new MemoryApplicationContext();
            clinicRepository = new Mock<ClinicRepository>(context.Context).Object;
            regionRepository = new Mock<RegionRepository>(context.Context).Object;
            workModeRepository = new Mock<WorkModeRepository>(context.Context).Object;

            virusCaseRepository = new Mock<VirusCaseRepository>(context.Context).Object;
            var mock = new Mock<ILogger<VirusController>>();

            ILogger<VirusController> logger = mock.Object;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "example name"),
                    new Claim(ClaimTypes.NameIdentifier, "1"),
                    new Claim("custom-claim", "example claim value"),
                }, "mock"));


            virusController = new VirusController(virusCaseRepository, clinicRepository, regionRepository, workModeRepository, logger,context.UserManager);
            virusController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
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
