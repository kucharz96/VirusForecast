using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirusForecast.Models;

namespace VirusForecast.Data.Interfaces
{
    public interface IWorkModeRepository
    {
        List<WorkMode> GetAll();
        WorkMode GetByName(string name);
        string GetName(string id);
    }
}
