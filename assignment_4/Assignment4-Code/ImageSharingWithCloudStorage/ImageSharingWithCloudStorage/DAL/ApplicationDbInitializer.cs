using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using ImageSharingWithCloudStorage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ImageSharingWithCloudStorage.DAL
{
    public  class ApplicationDbInitializer
    {
        private ApplicationDbContext db;
        private ILogContext logs;
        private ILogger<ApplicationDbInitializer> logger;

        public ApplicationDbInitializer(ApplicationDbContext db, ILogContext logs, ILogger<ApplicationDbInitializer> logger)
        {
            this.db = db;
            this.logs = logs;
            this.logger = logger;
        }

        public async Task SeedDatabase(IServiceProvider serviceProvider)
        {
            /*
             * Create image views log it doesn't already exist
             */
            //await logs.CreateTableAsync();
            db.Database.Migrate();

            try
            {
                db.RemoveRange(db.Images);
                db.RemoveRange(db.Tags);
                db.RemoveRange(db.Users);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to delete tables from database, may have not previously existed.");
            }

            db.SaveChanges();

            logger.LogInformation("Adding role: User");
            var idResult = await CreateRole(serviceProvider, "User");
            if (!idResult.Succeeded)
            {
                logger.LogError("Failed to create User role!");
            }

            // TODO add other roles
            await CreateRole(serviceProvider, "Admin");
            await CreateRole(serviceProvider, "Approver");
            await CreateRole(serviceProvider, "Supervisor"); 

            logger.LogInformation("Adding user: admin");
            idResult = await CreateAccount(serviceProvider, "admin", "admin", "Admin");
            if (!idResult.Succeeded)
            {
                logger.LogError("Failed to create admin user!");
            }

            logger.LogInformation("Adding user: approver");
            idResult = await CreateAccount(serviceProvider, "approver", "approver", "Approver");
            if (!idResult.Succeeded)
            {
                logger.LogError("Failed to create approver user!");
            }

            // TODO add other users and assign more roles

            logger.LogInformation("Adding user: user");
            idResult = await CreateAccount(serviceProvider, "user", "user", "User");
            if (!idResult.Succeeded)
            {
                logger.LogError("Failed to create user user!");
            }

            logger.LogInformation("Adding user: supervisor");
            idResult = await CreateAccount(serviceProvider, "supervisor", "supervisor", "Supervisor");
            if (!idResult.Succeeded)
            {
                logger.LogError("Failed to create supervisor user!");
            }

            Tag portrait = new Tag { Name = "portrait" };
            db.Tags.Add(portrait);
            Tag architecture = new Tag { Name = "architecture" };
            db.Tags.Add(architecture);

            // TODO add other tags

            db.SaveChanges();

        }

        public static async Task<IdentityResult> CreateRole(IServiceProvider provider,
                                                            string role)
        {
            RoleManager<IdentityRole> roleManager = provider
                .GetRequiredService
                       <RoleManager<IdentityRole>>();
            var idResult = IdentityResult.Success;
            if (await roleManager.FindByNameAsync(role) == null)
            {
                idResult = await roleManager.CreateAsync(new IdentityRole(role));
            }
            return idResult;
        }

        public static async Task<IdentityResult> CreateAccount(IServiceProvider provider,
                                                               string email, 
                                                               string password,
                                                               string role)
        {
            UserManager<ApplicationUser> userManager = provider
                .GetRequiredService
                       <UserManager<ApplicationUser>>();
            var idResult = IdentityResult.Success;

            if (await userManager.FindByNameAsync(email) == null)
            {
                ApplicationUser user = new ApplicationUser { UserName = email, Email = email };
                idResult = await userManager.CreateAsync(user, password);

                if (idResult.Succeeded)
                {
                    idResult = await userManager.AddToRoleAsync(user, role);
                }
            }

            return idResult;
        }

    }
}