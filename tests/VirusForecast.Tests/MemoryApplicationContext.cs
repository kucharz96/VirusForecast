using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
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
using VirusForecast.Models;

namespace VirusForecast.Tests
{
    public class MemoryApplicationContext
    {
        public ApplicationDbContext Context;
        public UserManager<User> UserManager;
        public SignInManager<User> SignInManager;

        public MemoryApplicationContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: "memory_db")
             .Options;

            Context = new ApplicationDbContext(options);
            var userStore = new UserStore<User>(Context);

            var userManagerMock = new Mock<UserManager<User>>(
                userStore,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<User>>().Object,
                new IUserValidator<User>[0],
                new IPasswordValidator<User>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<User>>>().Object);
            userManagerMock.Object.UserValidators.Add(new UserValidator<User>());
            userManagerMock.Object.PasswordValidators.Add(new PasswordValidator<User>());
            userManagerMock.Setup(x => x.DeleteAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<User, string>((x, y) => { Context.Users.Add(x); Context.SaveChanges(); });
            userManagerMock.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            UserManager = userManagerMock.Object;


            var signInManagerMock = new Mock<SignInManager<User>>(
               UserManager,
               new HttpContextAccessor(),
               new Mock<IUserClaimsPrincipalFactory<User>>().Object,
               new Mock<IOptions<IdentityOptions>>().Object,
               new Mock<ILogger<SignInManager<User>>>().Object,
               new Mock<IAuthenticationSchemeProvider>().Object,
               new Mock<IUserConfirmation<User>>().Object);


            signInManagerMock.Setup(x => x.PasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(SignInResult.Success);
            SignInManager = signInManagerMock.Object;




        }


    }
}
