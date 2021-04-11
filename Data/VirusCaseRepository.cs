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
    }
}
