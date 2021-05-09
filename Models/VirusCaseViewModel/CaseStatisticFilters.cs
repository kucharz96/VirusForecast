using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirusForecast.Models.VirusCaseViewModel
{
    public class CaseStatisticFilters
    {

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public char? Gender { get; set; }
        public string Region { get; set; }
        public string WorkMode { get; set; }
        public int? ChildrenAmount { get; set; }




    }
}