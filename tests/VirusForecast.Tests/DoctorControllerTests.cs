using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirusForecast.Controllers;
using VirusForecast.Data;
using VirusForecast.Data.Interfaces;
using VirusForecast.Models;
using VirusForecast.Models.DoctorViewModel;
using Xunit;

namespace VirusForecast.Tests
{
    public class DoctorControllerTests
    {
        private readonly ILogger<DoctorController> logger;
        private readonly DoctorController doctorController;
        private readonly IDoctorRepository _doctorRepository;
        public DoctorControllerTests()
        {

            MemoryApplicationContext context = new MemoryApplicationContext();


            var doctorRepository = new Mock<DoctorRepository>(context.Context, context.UserManager).Object;
            var clinicRepository = new Mock<ClinicRepository>(context.Context).Object;
            var mock = new Mock<ILogger<DoctorController>>();

            ILogger<DoctorController> logger = mock.Object;

            doctorController = new DoctorController(doctorRepository,clinicRepository, logger);
            _doctorRepository = new Mock<DoctorRepository>(context.Context, context.UserManager).Object;
        }

        [Fact]
        public void List()
        {
            var result = doctorController.List();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void List_CheckIfNotEmpty()
        {
            var result = doctorController.List();
            var viewResult = Assert.IsType<ViewResult>(result);
            var doctors = Assert.IsAssignableFrom<IEnumerable<DoctorListViewModel>>(viewResult.Model);
            Assert.NotNull(doctors);
        }
        
        [Fact]
        public async Task Add()
        {
            var newDoctor = new AddEditViewModel
            {
                Email = "mati@gmail.com",
                EmailConfirmed = true,
                Password = "12345678A.",
                ConfirmPassword = "12345678A."
            };

            await doctorController.Add(newDoctor);
            var testDoctor = this._doctorRepository.GetByEmail(newDoctor.Email);
            Assert.Equal(newDoctor.Email, testDoctor.Email);
        } 
    }
}
