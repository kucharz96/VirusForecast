using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirusForecast.Models
{
    public class VirusCase
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public char Gender { get; set; }
        public int ChildrenAmount { get; set; }
        public bool VirusPositive { get; set; }
        public Guid ClinicId { get; set; }
        public Guid RegionId { get; set; }
        public Guid WorkModeId { get; set; }
        public virtual Clinic Clinic { get; set; }
        public virtual WorkMode WorkMode { get; set; }
        public virtual Region Region { get; set; }

    }
}
