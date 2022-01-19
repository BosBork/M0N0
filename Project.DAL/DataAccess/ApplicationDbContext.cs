using Project.DAL;
using Project.DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DAL.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<VehicleMake> VehicleMakes { get; set; }
        public virtual DbSet<VehicleModel> VehicleModels { get; set; }

        #region old
        //public ApplicationDbContext()
        //{
        //} 
        #endregion

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region old
            //base.OnModelCreating(modelBuilder);

            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}

            //modelBuilder
            //    .Entity<VehicleMake>()
            //    .HasMany(x => x.VehicleModels)
            //    .WithOne(x => x.VehicleMake)
            //    .OnDelete(DeleteBehavior.Restrict);  
            #endregion

            modelBuilder.Entity<VehicleMake>().HasData(SeedExtension.SeedDataFromJson<VehicleMake>("MAKE_MOCK_DATA"));
            modelBuilder.Entity<VehicleModel>().HasData(SeedExtension.SeedDataFromJson<VehicleModel>("MODEL_MOCK_DATA"));
        }
    }
}
