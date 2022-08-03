namespace DataAccessLayer.Migrations
{
    using DataAccessLayer.Data.Context;
    using DataAccessLayer.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccessLayer.Data.Context.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccessLayer.Data.Context.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "Nour_Almeslmy",
                FirstName = "Nour",
                LastName = "Almeslmy",
                Email = "nouralmeslmy@gmail.com",
                Role = "admin",
                //DateJoined = DateTime.Now,
                PhoneNumber = "01276760353"
            };

            manager.Create(user, "password");
        }
    }
}
