using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirusForecast.Data.Interfaces;
using VirusForecast.Models;

namespace VirusForecast.Data
{
    public class RegionRepository : IRegionRepository
    {
        private readonly ApplicationDbContext _context;

        public RegionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Region> GetAll()
        {
            return _context.Regions.ToList();
        }

        public Region GetByName(string name)
        {
            return _context.Regions.Where(i => i.Name == name).FirstOrDefault();
        }

        public string GetName(string id)
        {
            return _context.Regions.Where(i => i.Id == id).FirstOrDefault().Name;
        }
    }
}
