using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.QueryServices.Users.Queries
{
    public class GetAllUsersQuery : IRequest<MultipleUsersQueryResponse>
    {
        public TravelUser CurrentUser { get; }
        public GetAllUsersQuery(TravelUser currentUser)
        {
            CurrentUser = currentUser;
        }
    }
}
