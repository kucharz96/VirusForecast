using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

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
        public void Add(string name)
        {
            var clinic = new Clinic();
            clinic.Name = name;
            _context.Add(clinic);
            _context.SaveChanges();
        }

        // <inheritdoc/>
        public Clinic Delete(string id)
        {
            var guid = new Guid(id);
            var clinic_to_delete = _context.Clinics.FirstOrDefault(x => x.Id.Equals(guid));
            if (clinic_to_delete != null)
            {
                _context.Remove(clinic_to_delete);
                _context.SaveChanges();
                return clinic_to_delete;
            }
            else
            {
                return null;
            }
        }

        // <inheritdoc/>
        public Clinic Edit(string id, string new_name)
        {
            var guid = new Guid(id);
            var clinic_to_edit = _context.Clinics.FirstOrDefault(x => x.Id.Equals(guid));
            if (clinic_to_edit != null)
            {
                clinic_to_edit.Name = new_name;
                _context.SaveChanges();
                return clinic_to_edit;
            }
            else
            {
                return null;
            }
        }

        // <inheritdoc/>
        public Clinic Get(string id)
        {
            var guid = new Guid(id);
            var clinic = _context.Clinics.FirstOrDefault(x => x.Id.Equals(guid));
            return clinic;
        }

        // <inheritdoc/>
        //public Clinic Get(string name)
        //{
        //    throw new NotImplementedException();
        //}

        // <inheritdoc/>
        public List<Clinic> GetAll()
        {
            return _context.Clinics.Include(clinic => clinic.Users).Include(clinic => clinic.VirusCases).ToList();
        }
    }
}
