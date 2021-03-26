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
        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<User> GetAll()
        {
            return (from bb in _context.Users.ToList()
                    join roleIds in _context.UserRoles.ToList() on bb.Id equals roleIds.UserId
                    join role in _context.Roles.ToList() on roleIds.RoleId equals role.Id into roles
                    where roles != null && roles.Any(e => e.Name == User.DOCTOR_ROLE)
                    select bb).ToList();
        }
    }
}
