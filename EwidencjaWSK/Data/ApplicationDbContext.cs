﻿using System;
using System.Collections.Generic;
using System.Text;
using EwidencjaWSK.Models;
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
        }

        public DbSet<Record> Records { get; set; }
        public DbSet<AdditionalDoc> AdditionalDocs{ get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Part> Parts { get; set; }

        public DbSet<RecordPart> RecordsParts { get; set; }
        public DbSet<RecordAdditionalDoc> RecordsAdditionalDocs { get; set; }

    }
}
