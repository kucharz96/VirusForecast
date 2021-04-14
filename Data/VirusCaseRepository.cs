using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirusForecast.Data.Interfaces;
using VirusForecast.Models;

namespace VirusForecast.Data
{
    public class VirusCaseRepository : IVirusCaseRepository
    {
        private readonly ApplicationDbContext _context;

        public VirusCaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public void Add(VirusCase virusCase)
        {
            _context.VirusCases.Add(virusCase);
            _context.SaveChanges();
        }

        public IEnumerable<VirusCase> GetDoctorsVirusCases(string doctorId)
        {
            var doctor = _context.Users.Where(x => x.Id == doctorId).FirstOrDefault();
            if (doctor != null)
                return _context.VirusCases.Where(x => x.ClinicId == doctor.ClinicId);
            else
                return null;
        }

        public string GetClinicName(string id)
        {
            var clinic = _context.Clinics.FirstOrDefault(x => x.Id.Equals(id)).Name;
            return clinic;
        }

        public void Delete(string id)
        {
            var virusCase = _context.VirusCases.FirstOrDefault(x => x.Id.Equals(id));
            _context.Remove(virusCase);
            _context.SaveChanges();
        }

        public IEnumerable<VirusCase> GetAll()
        {
            return _context.VirusCases;
        }
    }
}
