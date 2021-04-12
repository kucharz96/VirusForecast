using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VirusForecast.Models.VirusCaseViewModel
{
    public class VirusCaseListViewModel
    {
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public char Gender { get; set; }

        [Required]
        [Display(Name = "Children Amount")]
        public int ChildrenAmount { get; set; }

        [Required]
        [Display(Name = "Virus Positive")]
        public bool VirusPositive { get; set; }

        [Required]
        [Display(Name = "Clinic")]
        public string ClinicId { get; set; }

        [Required]
        [Display(Name = "Region")]
        public string RegionId { get; set; }

        [Required]
        [Display(Name = "Work Mode")]
        public string WorkModeId { get; set; }
    }
}
