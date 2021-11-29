using EntitiesCL.EFModels;
using EntitiesCL.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntitiesCL.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<VehicleMake> VehicleMakes { get; set; }
        public virtual DbSet<VehicleModel> VehicleModels { get; set; }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleMake>().HasData(SeedExtension.SeedDataFromJson<VehicleMake>("MAKE_MOCK_DATA"));
            modelBuilder.Entity<VehicleModel>().HasData(SeedExtension.SeedDataFromJson<VehicleModel>("MODEL_MOCK_DATA"));
        }
    }
}
