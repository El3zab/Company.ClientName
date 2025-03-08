using Company.ClientName.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company.ClientName.DAL.Data.Contexts
{
    public class CompanyDbContext : DbContext
    {
        // CLR
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = .; Database = CompanyProject; Trusted_Connection = True; TrustServerCertificate = True");
        //}

        public DbSet<Department> Departments { get; set; }
    }
}
