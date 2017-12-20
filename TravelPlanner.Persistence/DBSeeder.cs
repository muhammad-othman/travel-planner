using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.Persistence
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

            if (!_context.Roles.Any())
            {
                _context.Roles.Add(new UserRole { Name = "admin", NormalizedName = "admin" });
                _context.Roles.Add(new UserRole { Name = "manager", NormalizedName = "manager" });
            }

            //var admin = new TravelUser
            //{
            //    Email = "admin@travelplanner.com",
            //    NormalizedEmail = "admin@travelplanner.com",
            //    EmailConfirmed = true,
            //    LockoutEnabled = true,
            //    CreationDate = DateTime.Now,
            //    SecurityStamp = Guid.NewGuid().ToString()
            //};
            //var manager = new TravelUser
            //{
            //    Email = "manager@travelplanner.com",
            //    NormalizedEmail = "manager@travelplanner.com",
            //    EmailConfirmed = true,
            //    LockoutEnabled = true,
            //    CreationDate = DateTime.Now,
            //    SecurityStamp = Guid.NewGuid().ToString()
            //};
            //var normal = new TravelUser
            //{
            //    Email = "normal@travelplanner.com",
            //    NormalizedEmail = "normal@travelplanner.com",
            //    EmailConfirmed = true,
            //    LockoutEnabled = true,
            //    CreationDate = DateTime.Now,
            //    SecurityStamp = Guid.NewGuid().ToString()
            //};

            

            //if (!_context.Users.Any())
            //{
            //    var password = new PasswordHasher<TravelUser>();
            //    var hashed = password.HashPassword(admin, "password");
            //    admin.PasswordHash = hashed;
            //    manager.PasswordHash = hashed;
            //    normal.PasswordHash = hashed;
            //    var userStore = new UserStore<TravelUser>(_context);
            //    await userStore.CreateAsync(admin);
            //    await userStore.CreateAsync(manager);
            //    await userStore.CreateAsync(normal);
            //    await userStore.AddToRoleAsync(admin, "admin");
            //    await userStore.AddToRoleAsync(manager, "manager");
            //}
            _context.SaveChanges();
        }
    }
}
