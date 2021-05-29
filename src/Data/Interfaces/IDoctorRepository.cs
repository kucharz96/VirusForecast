using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirusForecast.Models;
using VirusForecast.Models.DoctorViewModel;

namespace VirusForecast.Data.Interfaces
{
    public interface IDoctorRepository
    {
        List<User> GetAll();
        Task<IEnumerable<IdentityError>> Add(User user, string password);
        User Get(string id);
        User GetByEmail(string email);
        void Edit(AddEditViewModel model);
        void Delete(string id);
        bool CheckIfEmailConfirmed(string email);
    }
}
