using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirusForecast.Data.Interfaces;
using VirusForecast.Models;

namespace VirusForecast.Data
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public DoctorRepository(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<IdentityError>> Add(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var result1 = await _userManager.AddToRoleAsync(user, Models.User.DOCTOR_ROLE);
                if (!result1.Succeeded)
                {
                    return result1.Errors;

                }
                return null;

            }
            return result.Errors;


        }

        public List<User> GetAll()
        {
            var values = (from bb in _context.Users.Include(clinic=>clinic.Clinic).ToList()
                          join roleIds in _context.UserRoles.ToList() on bb.Id equals roleIds.UserId
                          join role in _context.Roles.ToList() on roleIds.RoleId equals role.Id into roles
                          where roles != null && roles.Any(e => e.Name == User.DOCTOR_ROLE)
                          select bb);
            return values.ToList();
        }




    }
}
