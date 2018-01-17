using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TravelPlanner.CommandsServices.Users;
using TravelPlanner.Persistence;
using TravelPlanner.Presentation.IdentityCustomeStores;
using TravelPlanner.QueryServices.Roles;
using TravelPlanner.QueryServices.Users;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.Presentation
{
    public class DBSeeder
    {
        private readonly TravelPlannerContext _context;
        private readonly IUsersWriteService _usersWriteService;
        private readonly IUsersReadService _usersReadService;
        private readonly IRolesReadService _rolesReadService;

        public DBSeeder(IUsersReadService usersReadService, IUsersWriteService usersWriteService,
            IRolesReadService rolesReadService, TravelPlannerContext travelPlannerContext)
        {
            _context = travelPlannerContext;
            _rolesReadService = rolesReadService;

            _usersReadService = usersReadService;
            _usersWriteService = usersWriteService;
        }
        public async Task Seed()
        {

            if (!_context.Roles.Any())
            {
                _context.Roles.Add(new UserRole { Name = "admin", NormalizedName = "admin" });
                _context.Roles.Add(new UserRole { Name = "manager", NormalizedName = "manager" });
            }

            var admin = new TravelUser
            {
                Email = "admin@travelplanner.com",
                NormalizedEmail = "admin@travelplanner.com",
                EmailConfirmed = true,
                LockoutEnabled = true,
                CreationDate = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var manager = new TravelUser
            {
                Email = "manager@travelplanner.com",
                NormalizedEmail = "manager@travelplanner.com",
                EmailConfirmed = true,
                LockoutEnabled = true,
                CreationDate = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var normal = new TravelUser
            {
                Email = "normal@travelplanner.com",
                NormalizedEmail = "normal@travelplanner.com",
                EmailConfirmed = true,
                LockoutEnabled = true,
                CreationDate = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            if (!_context.Users.Any())
            {
                var password = new PasswordHasher<TravelUser>();
                var hashed = password.HashPassword(admin, "password");
                admin.PasswordHash = hashed;
                manager.PasswordHash = hashed;
                normal.PasswordHash = hashed;
                var userStore = new TravelUserStore(_usersReadService, _usersWriteService,_rolesReadService);
                await userStore.CreateAsync(admin, new CancellationToken());
                await userStore.CreateAsync(manager, new CancellationToken());
                await userStore.CreateAsync(normal, new CancellationToken());
                await userStore.AddToRoleAsync(admin, "admin", new CancellationToken());
                await userStore.AddToRoleAsync(manager, "manager", new CancellationToken());
            }
            _context.SaveChanges();
        }
    }
}
