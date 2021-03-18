using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirusForecast.Models
{
    public class User : IdentityUser
    {
        public const string DOCTOR_ROLE = "Doctor";
    }
}
