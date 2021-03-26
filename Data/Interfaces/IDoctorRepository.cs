using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirusForecast.Models;

namespace VirusForecast.Data.Interfaces
{
    public interface IDoctorRepository
    {
        List<User> GetAll();
        Task<IEnumerable<IdentityError>> Add(User user, string password);
    }
}
