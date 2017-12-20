using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;
using TravelPlanner.Shared.Enums;

namespace TravelPlanner.QueryServices.Roles
{
    public class MultipleRolesQueryResponse
    {
        public Result Result { get; set; }
        public ICollection<string> Errors { get; set; }
        public ICollection<UserRole> Roles { get; }

        public MultipleRolesQueryResponse(ICollection<UserRole> roles)
        {
            Errors = new List<string>();
            Roles = roles;
        }
    }
}
