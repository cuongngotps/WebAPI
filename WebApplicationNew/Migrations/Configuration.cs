namespace WebApplicationNew.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApplicationNew.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplicationNew.DB.DemoDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WebApplicationNew.DB.DemoDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));


            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }


            manager.Create(new ApplicationUser()
            {
                UserName = "user1",
                Email = "user1@demo.vn"
            }, "User111");

            var user1 = manager.FindByEmail("user1@demo.vn");
            manager.AddToRoles(user1.Id, new string[] { "User" });
            

            manager.Create(new ApplicationUser()
            {
                UserName = "admin",
                Email = "admin@demo.vn"
            }, "Admin1");

            var admin = manager.FindByEmail("admin@demo.vn");

            manager.AddToRoles(admin.Id, new string[] { "Admin" });

            base.Seed(context);
        }
    }
}
