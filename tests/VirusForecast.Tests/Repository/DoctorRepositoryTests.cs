using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirusForecast.Data;
using VirusForecast.Data.Interfaces;
using VirusForecast.Models;
using VirusForecast.Tests.Data;
using Xunit;

namespace VirusForecast.Tests.Repository
{
    /// <summary>
    /// Klasa testów do repozytorium lekarzy.
    /// </summary>
    public class DoctorRepositoryTests
    {
        /// <summary>
        /// Test dodania nowego lekarz do repozytorium.
        /// </summary>
        [Fact]
        public async Task AddDoctorTestAsync()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            IDoctorRepository repository = new DoctorRepository(dbContext, MockUserManager(new List<User>()).Object);
            IClinicRepository clinic_repository = new ClinicRepository(dbContext);
            var savedClinic = clinic_repository.Add("Klinika");

            var user = new User()
            {
                UserName = "User",
                Email = "user@email.com",
                EmailConfirmed = false,
                ClinicId = savedClinic.Id,
            };

            // Act
            var errors = await repository.Add(user, "user123");

            // Assert
            var created = dbContext.Users.FirstOrDefault();
            Assert.NotNull(created);
            Assert.Null(errors);
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

        private static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.AddToRoleAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }
    }
}
