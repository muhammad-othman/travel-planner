using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;
using TravelPlanner.Shared.Enums;

namespace TravelPlanner.QueryServices.Roles
{
    public class SingleRoleQueryResponse
    {
        public Result Result { get; set; }
        public ICollection<string> Errors { get; set; }
        public UserRole Role { get; }

        public SingleRoleQueryResponse(UserRole role)
        {
            Errors = new List<string>();
            Role = role;
        }
    }
}
