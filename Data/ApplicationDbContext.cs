using System;
using System.Collections.Generic;
using System.Text;
using HepsiburadaCase.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HepsiburadaCase.Data {
    public class ApplicationDbContext : IdentityDbContext {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }

        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }
    }
}