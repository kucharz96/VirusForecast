using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VirusForecast.Models;

namespace VirusForecast.Helpers
{
    public sealed class VirusCaseMap : ClassMap<VirusCase>
    {
        public VirusCaseMap()
        {
            Map(x => x.Age).Name("Age");
            Map(x => x.Region.Name).Name("Region");
            Map(x => x.WorkMode.Name).Name("WorkMode");
            Map(x => x.ChildrenAmount).Name("ChildrenAmount");
            Map(x => x.Gender).Name("Gender");
            Map(x => x.VirusPositive).Name("VirusPositive");

        }

        public static List<VirusCase> ReadCSVFile(Stream stream)
        {
            try
            {
                using (var reader = new StreamReader(stream))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.RegisterClassMap<VirusCaseMap>();
                    var records = csv.GetRecords<VirusCase>().ToList();
                    return records;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
