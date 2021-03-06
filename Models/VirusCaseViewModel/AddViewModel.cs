using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirusForecast.Models.VirusCaseViewModel
{
    public class AddViewModel
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Required]
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
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Clinic")]
        public string ClinicId { get; set; }

        [Required]
        [Display(Name = "Region")]
        public string RegionId { get; set; }

        [Required]
        [Display(Name = "Work Mode")]
        public string WorkModeId { get; set; }

        public List<SelectListItem> Regions { set; get; }

        public List<SelectListItem> WorkdModes { set; get; }

        public List<SelectListItem> Clinics { set; get; }
    }
}
