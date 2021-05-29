using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirusForecast.Data;
using VirusForecast.Data.Interfaces;
using VirusForecast.Models;
using Xunit;

namespace VirusForecast.Tests.Repository
{
    /// <summary>
    /// Klasa testów do repozytorium lekarzy.
    /// </summary>
    public class DoctorRepositoryTests
    {
        private IDoctorRepository doctorRepository;
        private IClinicRepository clinicRepository;

        public DoctorRepositoryTests()
        {
            MemoryApplicationContext context = new MemoryApplicationContext();
            
            doctorRepository = new Mock<DoctorRepository>(context.Context, context.UserManager).Object;
            clinicRepository = new Mock<ClinicRepository>(context.Context).Object;
        }


        /// <summary>
        /// Test dodania nowego lekarz do repozytorium.
        /// </summary>
        [Fact]
        public async Task AddDoctorTestAsync()
        {
            Guid id = Guid.NewGuid();
            var user = new User()
            {
                Id = id.ToString(),
                UserName = "User",
                Email = "user@email.com",
                EmailConfirmed = false,
                ClinicId = null
            };

            // Act
            var errors = await doctorRepository.Add(user, "user123");
            var userTest = doctorRepository.Get(id.ToString());

            Assert.NotNull(userTest);

        }

    }
}
