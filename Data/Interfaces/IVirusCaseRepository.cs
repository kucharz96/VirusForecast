using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirusForecast.Models;
using VirusForecast.Models.VirusCaseViewModel;

namespace VirusForecast.Data.Interfaces
{
    public interface IVirusCaseRepository
    {
        void Add(VirusCase virusCase);

        IEnumerable<VirusCase> GetDoctorsVirusCases(string doctorId);
        IEnumerable<VirusCase> GetAll();

        string GetClinicName(string id);

        void Delete(string id);

        VirusCase Get(string id);

        void Edit(AddViewModel model);

        List<CaseStatisic> GetRealCases(CaseStatisticFilters filters);
        List<CaseStatisic> GetForecastCases(CaseStatisticFilters filters);



    }
}
