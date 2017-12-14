using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPlanner.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace TravelPlanner.Model
{
    public class TravelPlannerContext : IdentityDbContext<TravelUser>
    {
        public TravelPlannerContext(DbContextOptions options) : base(options)
        {
        }

        public TravelPlannerContext()
        {

        }
        public virtual DbSet<TravelUser> TravelUsers { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
    }
}
