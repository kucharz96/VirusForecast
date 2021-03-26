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

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<User>()
        //        .HasOne<Clinic>(e => e.Clinic)
        //        .WithMany(d => d.Users)
        //        .HasForeignKey(e => e.ClinicId);

        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLazyLoadingProxies();
        //}

    }
}
