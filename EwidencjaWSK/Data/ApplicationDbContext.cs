using System;
using System.Collections.Generic;
using System.Text;
using EwidencjaWSK.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EwidencjaWSK.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Record> Records { get; set; }
        public DbSet<WarehouseDoc> WarehouseDocs { get; set; }
        public DbSet<AccountDoc> AccountDocs { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Part> Parts { get; set; }

    }
}
