using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Seregin_Backend.Models;

namespace Seregin_Backend.Data
{
    public class BuildingContext : DbContext
    {
        public BuildingContext(DbContextOptions<BuildingContext> options)
            : base(options)
        {
        }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Building>().ToTable("Building");
            modelBuilder.Entity<Apartment>().ToTable("Apartment");
            modelBuilder.Entity<DesignProject>().ToTable("DesignProject");
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<Apartment>()
            .HasOne(p => p.Bldng)
            .WithMany(b => b.Apts)
            .HasForeignKey(p => p.InBldngID);

            modelBuilder.Entity<DesignProject>()
            .HasOne(p => p.Apt)
            .WithMany(b => b.ProjectsForApt)
            .HasForeignKey(p => p.AptID);

            modelBuilder.Entity<DesignProject>()
            .HasOne(p => p.Usr)
            .WithMany(b => b.Projects)
            .HasForeignKey(p => p.UserID);

            
        }
    }
}
