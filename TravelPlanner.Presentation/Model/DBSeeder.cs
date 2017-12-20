using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPlanner.Presentation.Model.Entities;

namespace TravelPlanner.Presentation.Model
{
    public class DBSeeder
    {
        private readonly TravelPlannerContext _context;

        public DBSeeder(TravelPlannerContext travelPlannerContext)
        {
            _context = travelPlannerContext;
        }

        public async Task Seed()
        {
            var admin = new TravelUser
            {
                UserName = "admin@travelplanner.com",
                NormalizedUserName = "admin@travelplanner.com",
                Email = "admin@travelplanner.com",
                NormalizedEmail = "admin@travelplanner.com",
                EmailConfirmed = true,
                LockoutEnabled = true,
                CreationDate = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var manager = new TravelUser
            {
                UserName = "manager@travelplanner.com",
                NormalizedUserName = "manager@travelplanner.com",
                Email = "manager@travelplanner.com",
                NormalizedEmail = "manager@travelplanner.com",
                EmailConfirmed = true,
                LockoutEnabled = true,
                CreationDate = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var normal = new TravelUser
            {
                UserName = "normal@travelplanner.com",
                NormalizedUserName = "normal@travelplanner.com",
                Email = "normal@travelplanner.com",
                NormalizedEmail = "normal@travelplanner.com",
                EmailConfirmed = true,
                LockoutEnabled = true,
                CreationDate = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var roleStore = new RoleStore<IdentityRole>(_context);

            if (!_context.Roles.Any())
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "admin", NormalizedName = "admin" });
                await roleStore.CreateAsync(new IdentityRole { Name = "manager", NormalizedName = "manager" });
            }

            if (!_context.Users.Any())
            {
                var password = new PasswordHasher<TravelUser>();
                var hashed = password.HashPassword(admin, "password");
                admin.PasswordHash = hashed;
                manager.PasswordHash = hashed;
                normal.PasswordHash = hashed;
                var userStore = new UserStore<TravelUser>(_context);
                await userStore.CreateAsync(admin);
                await userStore.CreateAsync(manager);
                await userStore.CreateAsync(normal);
                await userStore.AddToRoleAsync(admin, "admin");
                await userStore.AddToRoleAsync(manager, "manager");
            }
            _context.SaveChanges();
        }
    }
}
