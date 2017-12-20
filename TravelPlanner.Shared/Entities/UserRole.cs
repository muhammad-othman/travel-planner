using System;
using System.Collections.Generic;
using System.Text;

namespace TravelPlanner.Shared.Entities
{
    public class UserRole
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string NormalizedName { get; set; }
        public virtual string ConcurrencyStamp { get; set; }
    }
}
