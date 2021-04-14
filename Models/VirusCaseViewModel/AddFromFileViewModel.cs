using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirusForecast.Models.VirusCaseViewModel
{
    public class AddFromFileViewModel
    {
        [Required]
        public IFormFile File { set; get; }

        public List<SelectListItem> Clinics { set; get; }


        [Required]
        [Display(Name = "Clinic")]
        public string ClinicId { get; set; }
    }
}
