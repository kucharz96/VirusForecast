using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirusForecast.Data.Interfaces;
using VirusForecast.Models;
using VirusForecast.Models.VirusCaseViewModel;

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

        public VirusCase Get(string id)
        {
            return _context.VirusCases.FirstOrDefault(x => x.Id.Equals(id));
        }

        public void Edit(AddViewModel model)
        {
            var virusCase = _context.VirusCases.FirstOrDefault(x => x.Id.Equals(model.Id));
            virusCase.Gender = model.Gender;
            virusCase.Age = model.Age;
            virusCase.ChildrenAmount = model.ChildrenAmount;
            virusCase.RegionId = model.RegionId;
            virusCase.WorkModeId = model.WorkModeId;
            virusCase.ClinicId = model.ClinicId;
            virusCase.VirusPositive = model.VirusPositive;
            _context.SaveChanges();
        }

        public IEnumerable<VirusCase> GetAll()
        {
            return _context.VirusCases;
        }

        public List<CaseStatisic> GetRealCases(CaseStatisticFilters filters)
        {

            var cases = _context.VirusCases.Where(i=>i.VirusPositive == true);
            if (filters.DateFrom.HasValue)
            {
                cases.Where(i => i.Date >= filters.DateFrom);
            }
            if (filters.DateTo.HasValue)
            {
                if (filters.DateTo > DateTime.Now.Date)
                {
                    cases.Where(i => i.Date <= DateTime.Now.Date);
                }
                else
                {
                    cases.Where(i => i.Date <= filters.DateTo);
                }
            }


            var statisics = cases.GroupBy(i => i.Date)
                .Select(y => new CaseStatisic
                {
                    CasesCount = y.Count(),
                    Date = y.Key

                })
                .OrderBy(i=>i.Date)
                .ToList();




            return statisics;


        }

        public List<CaseStatisic> GetForecastCases(CaseStatisticFilters filters)
        {
            if(filters.DateTo.HasValue && filters.DateTo <= DateTime.Now.Date)
            {
                return new List<CaseStatisic>();
            }
            else
            {
                DateTime generateDateFrom;
                DateTime generateDateTo;


                if(!filters.DateFrom.HasValue || filters.DateFrom <= DateTime.Now.Date)
                {
                    generateDateFrom = DateTime.Now.Date.AddDays(1);
                }
                else
                {
                    generateDateFrom = filters.DateFrom.Value;
                }

                if (!filters.DateTo.HasValue)
                {
                    generateDateTo = DateTime.Now.Date.AddDays(8);
                }
                else
                {
                    generateDateTo = filters.DateFrom.Value;
                }

                var statistic = new List<CaseStatisic>();

                for(var startDate = generateDateFrom; startDate < generateDateTo; startDate = startDate.AddDays(1))
                {
                    Random rnd = new Random();
                    int caseNumber = rnd.Next(11);
                    statistic.Add(new CaseStatisic
                    {
                        CasesCount = caseNumber,
                        Date = startDate
                    });


                }

                return statistic;



            }


        }
    }
}
