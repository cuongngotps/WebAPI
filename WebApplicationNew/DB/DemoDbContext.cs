using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplicationNew.Models;

namespace WebApplicationNew.DB
{
    public class DemoDbContext : IdentityDbContext<ApplicationUser>
    {
        public DemoDbContext() : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DemoDbContext, WebApplicationNew.Migrations.Configuration>("DefaultConnection"));
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