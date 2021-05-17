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
        /// Test dodania nowego przypadku wirusa do repozytorium.
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
        /// Test podpięcia kliniki do przypadku wirusa.
        /// </summary>
        [Fact]
        public void BindClinicToVirusCaseTest()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            IVirusCaseRepository virusCaseRepository = new VirusCaseRepository(dbContext);
            IClinicRepository clinicRepository = new ClinicRepository(dbContext);
            var savedClinic = clinicRepository.Add("Klinika");
            VirusCase virusCase = new VirusCase();
            virusCase.Clinic = savedClinic;

            // Act
            virusCaseRepository.Add(virusCase);

            // Assert
            var created = dbContext.VirusCases.FirstOrDefault();
            Assert.NotNull(created);
            Assert.NotNull(created.Clinic);
            Assert.NotNull(created.ClinicId);
        }

        /// <summary>
        /// Test usuwania przypadku wirusa.
        /// </summary>
        [Fact]
        public void DeleteVirusCaseTest()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            IVirusCaseRepository virusCaseRepository = new VirusCaseRepository(dbContext);
            VirusCase virusCase = new VirusCase();
            virusCaseRepository.Add(virusCase);

            // Act
            virusCaseRepository.Delete(virusCase.Id);

            // Assert
            var created = dbContext.VirusCases.FirstOrDefault();
            Assert.Null(created);
        }

        /// <summary>
        /// Test pobrania przypadków wirusa dla określonego zakresu dat.
        /// </summary>
        [Fact]
        public void GetVirusRealCasesByDateFromAndDateToTest()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            IVirusCaseRepository virusCaseRepository = new VirusCaseRepository(dbContext);
            VirusCase virusCase1 = new VirusCase() { Date = new DateTime(2021, 3, 21), VirusPositive = true };
            VirusCase virusCase2 = new VirusCase() { Date = new DateTime(2021, 3, 4), VirusPositive = true };
            VirusCase virusCase3 = new VirusCase() { Date = new DateTime(2021, 3, 30), VirusPositive = true };
            VirusCase virusCase4 = new VirusCase() { Date = new DateTime(2021, 5, 5), VirusPositive = true };
            VirusCase virusCase5 = new VirusCase() { Date = new DateTime(2021, 1, 1), VirusPositive = true };
            VirusCase virusCase6 = new VirusCase() { Date = new DateTime(2021, 2, 13), VirusPositive = true };
            VirusCase virusCase7 = new VirusCase() { Date = new DateTime(2021, 3, 4), VirusPositive = true };

            virusCaseRepository.Add(virusCase1);
            virusCaseRepository.Add(virusCase2);
            virusCaseRepository.Add(virusCase3);
            virusCaseRepository.Add(virusCase4);
            virusCaseRepository.Add(virusCase5);
            virusCaseRepository.Add(virusCase6);
            virusCaseRepository.Add(virusCase7);

            // Act
            var filtered = virusCaseRepository.GetRealCases(new Models.VirusCaseViewModel.CaseStatisticFilters() { DateFrom = new DateTime(2021, 3, 1), DateTo = new DateTime(2021, 3, 5) });

            // Assert
            Assert.Equal(5, filtered.Count);
            Assert.Equal(0, filtered[0].CasesCount);
            Assert.Equal(0, filtered[1].CasesCount);
            Assert.Equal(0, filtered[2].CasesCount);
            Assert.Equal(2, filtered[3].CasesCount);
            Assert.Equal(0, filtered[4].CasesCount);
        }

        /// <summary>
        /// Test pobrania przypadków wirusa dla osób z określoną liczbą dzieci.
        /// </summary>
        [Fact]
        public void GetVirusRealCasesByChildrenAmountTest()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            IVirusCaseRepository virusCaseRepository = new VirusCaseRepository(dbContext);
            VirusCase virusCase1 = new VirusCase() { Date = new DateTime(2021, 3, 1), VirusPositive = true, ChildrenAmount = 5 };
            VirusCase virusCase2 = new VirusCase() { Date = new DateTime(2021, 3, 2), VirusPositive = true, ChildrenAmount = 2 };
            VirusCase virusCase3 = new VirusCase() { Date = new DateTime(2021, 3, 2), VirusPositive = true, ChildrenAmount = 0 };
            VirusCase virusCase4 = new VirusCase() { Date = new DateTime(2021, 5, 3), VirusPositive = true, ChildrenAmount = 2 };

            virusCaseRepository.Add(virusCase1);
            virusCaseRepository.Add(virusCase2);
            virusCaseRepository.Add(virusCase3);
            virusCaseRepository.Add(virusCase4);

            // Act
            var filtered = virusCaseRepository.GetRealCases(new Models.VirusCaseViewModel.CaseStatisticFilters()
            {
                DateFrom = new DateTime(2021, 3, 1),
                DateTo = new DateTime(2021, 3, 5),
                ChildrenAmount = 2
            });

            // Assert
            Assert.Equal(5, filtered.Count);
            Assert.Equal(0, filtered[0].CasesCount);
            Assert.Equal(1, filtered[1].CasesCount);
            Assert.Equal(0, filtered[2].CasesCount);
            Assert.Equal(0, filtered[3].CasesCount);
            Assert.Equal(0, filtered[4].CasesCount);
        }

        /// <summary>
        /// Test pobrania przypadków wirusa dla osób z określoną płcią.
        /// </summary>
        [Fact]
        public void GetVirusRealCasesByGenderTest()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            IVirusCaseRepository virusCaseRepository = new VirusCaseRepository(dbContext);
            VirusCase virusCase1 = new VirusCase() { Date = new DateTime(2021, 3, 1), VirusPositive = true, Gender = 'K' };
            VirusCase virusCase2 = new VirusCase() { Date = new DateTime(2021, 3, 2), VirusPositive = true, Gender = 'K' };
            VirusCase virusCase3 = new VirusCase() { Date = new DateTime(2021, 3, 2), VirusPositive = true, Gender = 'M' };
            VirusCase virusCase4 = new VirusCase() { Date = new DateTime(2021, 5, 3), VirusPositive = true, Gender = 'K' };

            virusCaseRepository.Add(virusCase1);
            virusCaseRepository.Add(virusCase2);
            virusCaseRepository.Add(virusCase3);
            virusCaseRepository.Add(virusCase4);

            // Act
            var filtered = virusCaseRepository.GetRealCases(new Models.VirusCaseViewModel.CaseStatisticFilters()
            {
                DateFrom = new DateTime(2021, 3, 1),
                DateTo = new DateTime(2021, 3, 5),
                Gender = 'K'
            });

            // Assert
            Assert.Equal(5, filtered.Count);
            Assert.Equal(1, filtered[0].CasesCount);
            Assert.Equal(1, filtered[1].CasesCount);
            Assert.Equal(0, filtered[2].CasesCount);
            Assert.Equal(0, filtered[3].CasesCount);
            Assert.Equal(0, filtered[4].CasesCount);
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
