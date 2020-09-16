using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EwidencjaWSK.Models;
using EwidencjaWSK.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EwidencjaWSK.Data
{
    //public class ApplicationDbContext : IdentityDbContext
    // odkomentować powyżej aby identity ruszyło
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecordPart>().HasKey(sc => new { sc.RecordId, sc.PartId });
            modelBuilder.Entity<RecordAdditionalDoc>().HasKey(sc => new { sc.RecordId, sc.AdditionalDocId });

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User", NormalizedName = "USER", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });

            base.OnModelCreating(modelBuilder);

            
        }

        public DbSet<Record> Records { get; set; }
        public DbSet<AdditionalDoc> AdditionalDocs{ get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Part> Parts { get; set; }

        public DbSet<RecordPart> RecordsParts { get; set; }
        public DbSet<RecordAdditionalDoc> RecordsAdditionalDocs { get; set; }

    }
}
