using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;
using TravelPlanner.Shared.Enums;

namespace TravelPlanner.CommandsServices.Users
{
    public class UserCommandResponse
    {
        public Result Result { get; set; }

        public ICollection<string> Errors { get; set; }
        public TravelUser User { get; }

        public UserCommandResponse(TravelUser user)
        {
            Errors = new List<string>();
            User = user;
        }
    }
}
