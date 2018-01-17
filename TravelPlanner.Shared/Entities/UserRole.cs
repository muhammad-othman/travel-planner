using System;
using System.Collections.Generic;
using System.Text;

namespace TravelPlanner.Shared.Entities
{
    public class UserRole
    {
        public UserRole()
        {
            TravelUsers = new List<TravelUser>();
        }
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string NormalizedName { get; set; }
        public virtual string ConcurrencyStamp { get; set; }
        public ICollection<TravelUser> TravelUsers { get; set; }
    }
}
