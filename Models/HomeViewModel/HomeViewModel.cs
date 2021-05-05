using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirusForecast.Models.HomeViewModel
{
    public class HomeViewModel
    {
        public List<SelectListItem> Regions { set; get; }

        public List<SelectListItem> WorkdModes { set; get; }
    }
}
