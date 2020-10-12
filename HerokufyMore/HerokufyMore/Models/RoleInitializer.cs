﻿using HerokufyMore.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerokufyMore.Models
{
    public class RoleInitializer
    {
        private static readonly List<IdentityRole> Roles = new List<IdentityRole>()
        {
            new IdentityRole
            {
                Name = ApplicationRoles.Admin,
                NormalizedName = ApplicationRoles.Admin.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },

            new IdentityRole
            {
                Name = ApplicationRoles.Member,
                NormalizedName = ApplicationRoles.Member.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }
        };

        public static void SeedData(IServiceProvider serviceProvider, UserManager<ApplicationUser> users, IConfiguration _configuration, IWebHostEnvironment _webHostEnvironment)
        {
            using var dbContext = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            dbContext.Database.EnsureCreated();
            AddRoles(dbContext);
            SeedUsersAsync(users, _configuration, _webHostEnvironment);
        }

        private static void SeedUsersAsync(UserManager<ApplicationUser> userManager, IConfiguration _configuration, IWebHostEnvironment _webHostEnvironment)
        {
            string adminEmail = _webHostEnvironment.IsDevelopment()
                ? _configuration["ADMIN_EMAIL"]
                : Environment.GetEnvironmentVariable("ADMIN_EMAIL");

            if (userManager.FindByEmailAsync(adminEmail).Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "Admin"
                };

                string districtManagerPassword = _webHostEnvironment.IsDevelopment()
                    ? _configuration["ADMIN_PASSWORD"]
                    : Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

                IdentityResult result = userManager.CreateAsync(user, _configuration["ADMIN_PASSWORD"]).Result;

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, ApplicationRoles.Admin).Wait();
            }
        }

        private static void AddRoles(ApplicationDbContext dbContext)
        {
            if (dbContext.Roles.Any()) return;

            foreach (var role in Roles)
            {
                dbContext.Roles.Add(role);
                dbContext.SaveChanges();
            }
        }
    }
}
