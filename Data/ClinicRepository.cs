using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirusForecast.Data.Interfaces;
using VirusForecast.Models;

namespace VirusForecast.Data
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly ApplicationDbContext _context;

        public ClinicRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // <inheritdoc/>
        public Clinic Delete(int id)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc/>
        public Clinic Edit(int id, string new_name)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc/>
        public Clinic Get(int id)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc/>
        public Clinic Get(string name)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc/>
        public List<Clinic> GetAll()
        {
            var clinics = _context.Clinics.ToList();
            var users = _context.Users.ToList();

            var query = from clinic in clinics
                        join user in users on clinic.Id equals user.ClinicId into result
                        from subuser in result.DefaultIfEmpty()
                        select clinic;

            return query.ToList();
        }
    }
}
