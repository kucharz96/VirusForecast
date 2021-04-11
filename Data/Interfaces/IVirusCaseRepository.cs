﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirusForecast.Models;

namespace VirusForecast.Data.Interfaces
{
    public interface IVirusCaseRepository
    {
        void Add(VirusCase virusCase);
    }
}