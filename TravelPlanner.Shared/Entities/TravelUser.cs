using System;
using System.Collections.Generic;

namespace TravelPlanner.Shared.Entities
{
    public class TravelUser
    {
        public TravelUser()
        {
            Trips = new List<Trip>();
            Logins = new List<TravelUserLoginInfo>();
        }
        public virtual string Id { get; set; }
        public virtual string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public string Picture { get; set; }
        public virtual DateTimeOffset? LockoutEnd { get; set; }
        public virtual string ConcurrencyStamp { get; set; }
        public virtual string SecurityStamp { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual bool EmailConfirmed { get; set; }
        public virtual string NormalizedEmail { get; set; }
        public virtual bool LockoutEnabled { get; set; }
        public virtual int AccessFailedCount { get; set; }
        public ICollection<Trip> Trips { get; set; }
        public ICollection<TravelUserLoginInfo> Logins { get; set; }
        public string Role { get; set; }
    }
}
