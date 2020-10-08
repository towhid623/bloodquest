namespace WebsiteBack.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebsiteBack.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebsiteBack.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WebsiteBack.Models.ApplicationDbContext context)
        {
            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
         
                var user = new ApplicationUser { UserName = "admin" };

                manager.Create(user, "password");
            }
        }
    }
}
