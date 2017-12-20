using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;
using TravelPlanner.Shared.Enums;

namespace TravelPlanner.CommandsServices.Roles
{
    public class RoleCommandResponse
    {
        public Result Result { get; set; }

        public ICollection<string> Errors { get; set; }
        public UserRole Role { get; }

        public RoleCommandResponse(UserRole role)
        {
            Errors = new List<string>();
            Role = role;
        }
    }
}
