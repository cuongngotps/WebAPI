using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplicationNew.Models;
using WebApplicationNew.Table;

namespace WebApplicationNew.DB
{
    public class DemoDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Hometown> Hometowns { get; set; }

        public DemoDbContext() : base("DatabaseConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public static DemoDbContext Create()
        {
            return new DemoDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}