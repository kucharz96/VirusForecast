using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirusForecast.Data.Interfaces;
using VirusForecast.Models;

namespace VirusForecast.Data
{
    public class WorkModeRepository : IWorkModeRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkModeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<WorkMode> GetAll()
        {
            return _context.WorkModes.ToList();
        }

        public WorkMode GetByName(string name)
        {
            return _context.WorkModes.Where(i => i.Name == name).FirstOrDefault();
        }
    }
}
