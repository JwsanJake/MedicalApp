using MedicalApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalApp.Data
{
    public class NpgsqlDbContext : DbContext
    {
        public NpgsqlDbContext(DbContextOptions<NpgsqlDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Patient>()
                .HasOne(m => m.MedicalCard)
                .WithOne(p => p.Patient)
                .HasForeignKey<MedicalCard>(k => k.MC_ID)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Visit>()
                .HasOne(m => m.MedicalCard)
                .WithMany(v => v.Visit)
                .HasForeignKey(k => k.MC_ID)
                .OnDelete(DeleteBehavior.Cascade);

        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet <MedicalCard> MedicalCards { get; set; }
        public DbSet<Visit> Visits { get; set; }

    }
}
