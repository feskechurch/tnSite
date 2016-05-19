using LandingPage.Controllers;
using LandingPage.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Collections.Generic;
using ApplicationUser = LandingPage.Models.ApplicationUser;
using System;

namespace LandingPage.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "LandingPage.Models.ApplicationUser";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //Creating roles
            var idManager = new IdentityManager();
            idManager.CreateRole("Admin");

            //The UserStore is ASP Identity's data layer. Wrap context with the UserStore.
            var userStore = new UserStore<ApplicationUser>(context);

            var userManager = new UserManager<ApplicationUser>(userStore);


            context.Users.Add(new ApplicationUser
            {
                Email = "saman.parsifar@gmail.com",
                UserName = "saman.parsifar@gmail.com",
                PasswordHash = "AO5hwlI4Glc1Vb1/p95TuolisQrq9sErOE8pydG6ZClSWSJd0SXyodBP6Qw0jJtOZA==",

            });
            context.News.Add(new News
            {
                Title = "Nya Layout",
                Content = "Peter hjälper oss att lansera vår nya moderna hemsida ",
                PublishedDate = DateTime.Parse("2016-01-07 00:00:00 ")

            }); context.News.Add(new News
            {
                Title = "Beta-fas status ",
                Content = "Vi fortsätter att säkra upp beta-fasen med fler viktiga pilot-bolag inom konsultbranschen och Private Equity ",
                PublishedDate = DateTime.Parse("2016-01-10 00:00:00 ")

            }); context.News.Add(new News
            {
                Title = "Ny kompetens till TNBI",
                Content = "Fredrik joinar vårt team och bidrar med många års erfarenhet från Bisnode- och UC-relaterade produkter ",
                PublishedDate = DateTime.Parse("2016-01-15 00:00:00 ")

            });

            context.SaveChanges();

            //Get the UserId only if the SecurityStamp is not set yet.
            var userId1 = context.Users.Where(x => x.Email == "saman.parsifar@gmail.com" && string.IsNullOrEmpty(x.SecurityStamp)).Select(x => x.Id).FirstOrDefault();

            //If the userId is not null, then the SecurityStamp needs updating.
            if (!string.IsNullOrEmpty(userId1)) userManager.UpdateSecurityStamp(userId1);

            idManager.AddUserToRole(userId1, "Admin");

            context.SaveChanges();
        }
    }
}
