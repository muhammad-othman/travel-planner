using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;
using TravelPlanner.Shared.Enums;

namespace TravelPlanner.QueryServices.Users
{
    public class SingleUserQueryResponse
    {
        public ResponseStatus Status { get; set; }
        public ICollection<string> Errors { get; set; }
        public TravelUser User { get; }

        public SingleUserQueryResponse(TravelUser user)
        {
            Errors = new List<string>();
            User = user;
        }
    }
}
