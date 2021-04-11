using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirusForecast.Models.VirusCaseViewModel
{
    public class AddFromFileViewModel
    {
        
        public IFormFile File { set; get; }
    }
}
