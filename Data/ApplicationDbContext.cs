using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VirusForecast.Models;

namespace VirusForecast.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<VirusCase> VirusCases { get; set; }
        public DbSet<WorkMode> WorkModes { get; set; }




        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

    }
}
