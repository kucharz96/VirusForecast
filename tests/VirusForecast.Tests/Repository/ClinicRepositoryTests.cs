using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

using VirusForecast.Data;
using VirusForecast.Data.Interfaces;
using Xunit;

namespace VirusForecast.Tests.Repository
{
    /// <summary>
    /// Klasa testów do repozytorium kliniki.
    /// </summary>
    public class ClinicRepositoryTests
    {
        /// <summary>
        /// Test dodania nowej kliniki do repozytorium.
        /// </summary>
        [Fact]
        public void AddClinicTest()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            IClinicRepository repository = new ClinicRepository(dbContext);

            // Act
            var savedClinic = repository.Add("Klinika");

            // Assert
            var clinic = dbContext.Clinics.FirstOrDefault(x => x.Name == "Klinika");
            Assert.NotNull(clinic);
        }

        /// <summary>
        /// Test edycji nazwy kliniki w respozytorium.
        /// </summary>
        [Fact]
        public void EditClinicTest()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            IClinicRepository repository = new ClinicRepository(dbContext);
            var savedClinic = repository.Add("Klinika");

            // Act
            var updatedClinic = repository.Edit(savedClinic.Id, "Nowa klinika");

            // Assert
            Assert.Equal("Nowa klinika", updatedClinic.Name);
        }

        // Test usuwania kliniki z repozytorium.
        [Fact]
        public void DeleteClinicTest()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            IClinicRepository repository = new ClinicRepository(dbContext);
            var savedClinic = repository.Add("Klinika");

            // Act
            var deleteClinic = repository.Delete(savedClinic.Id);

            // Assert
            var clinic = dbContext.Clinics.FirstOrDefault();
            Assert.Equal("Klinika", deleteClinic.Name);
            Assert.Null(clinic);
        }

        /// <summary>
        /// Inicjalizacja bazy danych w pamiêci.
        /// </summary>
        /// <returns></returns>
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
