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

            // Create shadow properties
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                  .Where(e => typeof(IAuditable).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime>("Created");

                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime>("Modified");

                modelBuilder.Entity(entityType.ClrType)
                    .Property<string>("CreatedBy");

                modelBuilder.Entity(entityType.ClrType)
                    .Property<string>("ModifiedBy");
            }

            base.OnModelCreating(modelBuilder);

            
        }

        public override int SaveChanges()
        {
            ApplyAuditInformation();
            return base.SaveChanges();
        }

        private void ApplyAuditInformation()
        {
            var modifiedEntities = ChangeTracker.Entries<IAuditable>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
            foreach (var entity in modifiedEntities)
            {
                entity.Property("Modified").CurrentValue = DateTime.UtcNow;
                if (entity.State == EntityState.Added)
                {
                    entity.Property("Created").CurrentValue = DateTime.UtcNow;
                }
            }
        }



        public DbSet<Record> Records { get; set; }
        public DbSet<AdditionalDoc> AdditionalDocs{ get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Part> Parts { get; set; }

        public DbSet<RecordPart> RecordsParts { get; set; }
        public DbSet<RecordAdditionalDoc> RecordsAdditionalDocs { get; set; }

    }
}
