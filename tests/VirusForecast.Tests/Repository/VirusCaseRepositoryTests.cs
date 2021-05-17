using Microsoft.EntityFrameworkCore;
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
    /// Klasa testów do repozytorium przypadków wirusa.
    /// </summary>
    public class VirusCaseRepositoryTests
    {
        /// <summary>
        /// Test dodania nowej kliniki do repozytorium.
        /// </summary>
        [Fact]
        public void AddVirusCaseTest()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            IVirusCaseRepository repository = new VirusCaseRepository(dbContext);
            VirusCase virusCase = new VirusCase();
            virusCase.Age = 30;
            virusCase.ChildrenAmount = 2;
            virusCase.VirusPositive = true;
            virusCase.Gender = 'K';
            virusCase.Date = DateTime.Now;

            // Act
            repository.Add(virusCase);

            // Assert
            var created = dbContext.VirusCases.FirstOrDefault();
            Assert.NotNull(created);
            Assert.Equal(virusCase.Age, created.Age);
            Assert.Equal(virusCase.ChildrenAmount, created.ChildrenAmount);
            Assert.Equal(virusCase.VirusPositive, created.VirusPositive);
            Assert.Equal(virusCase.Gender, created.Gender);
            Assert.Equal(virusCase.Date, created.Date);
        }

        /// <summary>
        /// Inicjalizacja bazy danych w pamięci.
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
