using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VirusForecast.Models
{
    public class VirusCase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public int Age { get; set; }
        public char Gender { get; set; }
        public int ChildrenAmount { get; set; }
        public bool VirusPositive { get; set; }
        public DateTime Date { get; set; }
        public string ClinicId { get; set; }
        public string RegionId { get; set; }
        public string WorkModeId { get; set; }
        public virtual Clinic Clinic { get; set; }
        public virtual WorkMode WorkMode { get; set; }
        public virtual Region Region { get; set; }

    }
}
