using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using cwagnerShoppingApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace cwagnerShoppingApp.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            if (!context.Users.Any(u => u.Email == "cwagner0604@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "cwagner0604@gmail.com",
                    Email = "cwagner0604@gmail.com",
                    FirstName = "Christina",
                    LastName = "Wagner",
                }, "Password1!");
            }
            var userId = userManager.FindByEmail("cwagner0604@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");
        }
    }
}