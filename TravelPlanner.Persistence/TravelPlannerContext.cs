using Microsoft.EntityFrameworkCore;
using System;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.Persistence
{
    public class TravelPlannerContext :DbContext
    {

        public TravelPlannerContext(DbContextOptions<TravelPlannerContext> options) : base(options)
        {
        }
        public DbSet<TravelUser> Users { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<Trip> Trips { get; set; }

    }
}
