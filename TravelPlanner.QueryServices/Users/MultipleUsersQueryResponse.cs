using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;
using TravelPlanner.Shared.Enums;

namespace TravelPlanner.QueryServices.Users
{
    public class MultipleUsersQueryResponse
    {
        public Result Result { get; set; }
        public ICollection<string> Errors { get; set; }
        public ICollection<TravelUser> Users { get; }

        public MultipleUsersQueryResponse(ICollection<TravelUser> users)
        {
            Errors = new List<string>();
            Users = users;
        }
    }
}
