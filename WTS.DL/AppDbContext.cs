using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WTS.DL.Entities;

namespace WTS.DL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public AppDbContext()
        {

        }
                    
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        
        public DbSet<Store> Stores { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            //Called by parameterless ctor Usually Migrations
            //var environmentName = Environment.GetEnvironmentVariable("EnvironmentName") ?? "local";

            optionsBuilder.UseSqlServer(
                new ConfigurationBuilder()
                    .SetBasePath(Path.GetDirectoryName(GetType().GetTypeInfo().Assembly.Location))
                    .AddJsonFile($"appsettings.json", false, false)
                    .Build()
                    .GetConnectionString("DefaultConnection")
            );
        }

        public override int SaveChanges()
        {
            var validationErrors = ChangeTracker
                .Entries<IValidatableObject>()
                .SelectMany(e => e.Entity.Validate(null))
                .Where(r => r != ValidationResult.Success);

            return base.SaveChanges();
        }

    }
}
