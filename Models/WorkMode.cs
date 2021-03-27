﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirusForecast.Models
{
    public class WorkMode
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<VirusCase> VirusCases { get; set; }
        
    }
}
