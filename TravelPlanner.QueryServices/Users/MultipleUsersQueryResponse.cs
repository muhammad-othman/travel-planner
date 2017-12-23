using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;
using TravelPlanner.Shared.Enums;

namespace TravelPlanner.QueryServices.Users
{
    public class MultipleUsersQueryResponse
    {
        public ResponseStatus Status { get; set; }
        public ICollection<string> Errors { get; set; }
        public ICollection<TravelUser> Users { get; }
        public int TotalCount { get; set; }

        public MultipleUsersQueryResponse(ICollection<TravelUser> users, int totalCount)
        {
            TotalCount = totalCount;
            Errors = new List<string>();
            Users = users;
        }
    }
}
