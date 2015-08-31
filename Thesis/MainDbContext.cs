using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Thesis.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Thesis
{
    public class MainDbContext: DbContext
    {
        public MainDbContext() : base ("name=DefaultConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Patient> Patient { get; set; }
        public DbSet<Nurse> Nurse { get; set; }
        public DbSet<FDR> FDR { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Users> Users { get; set;}
    }

}