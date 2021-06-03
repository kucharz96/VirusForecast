using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirusForecast.Models.DoctorViewModel
{
    public class EditViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Account confirmed")]
        public bool EmailConfirmed{ get; set; }

        public List<SelectListItem> Clinics { set; get; }


        [Required]
        [Display(Name = "Clinic")]
        public string ClinicId { get; set; }
    }
}
