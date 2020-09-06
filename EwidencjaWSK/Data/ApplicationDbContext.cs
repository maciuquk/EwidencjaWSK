using System;
using System.Collections.Generic;
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

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<RecordPart>().HasKey(sc => new { sc.RecordId, sc.PartId });
            modelbuilder.Entity<RecordAdditionalDoc>().HasKey(sc => new { sc.RecordId, sc.AdditionalDocId });

            modelbuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User", NormalizedName = "USER", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
            modelbuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });

            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<Record>().Property<DateTime?>("Created");
            modelbuilder.Entity<Record>().Property<DateTime?>("Modified");

            modelbuilder.Entity<Supplier>().Property<DateTime?>("Created");
            modelbuilder.Entity<Supplier>().Property<DateTime?>("Modified");

            modelbuilder.Entity<Part>().Property<DateTime?>("Created");
            modelbuilder.Entity<Part>().Property<DateTime?>("Modified");

            modelbuilder.Entity<AdditionalDoc>().Property<DateTime?>("Created");
            modelbuilder.Entity<AdditionalDoc>().Property<DateTime?>("Modified");
        }

        public override int SaveChanges()
        {
            foreach (var auditableEntity in ChangeTracker.Entries<IAuditable>())
            {
                if (auditableEntity.State == EntityState.Added ||
                    auditableEntity.State == EntityState.Modified)
                {
                    auditableEntity.Entity.Modified = DateTime.Now;

                    if (auditableEntity.State == EntityState.Added)
                    {
                        auditableEntity.Entity.Created = DateTime.Now;
                    }
                }
            }
            return base.SaveChanges();
        }

        public DbSet<Record> Records { get; set; }
        public DbSet<AdditionalDoc> AdditionalDocs{ get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Part> Parts { get; set; }

        public DbSet<RecordPart> RecordsParts { get; set; }
        public DbSet<RecordAdditionalDoc> RecordsAdditionalDocs { get; set; }

    }
}
