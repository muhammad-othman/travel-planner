using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelPlanner.QueryServices.Users.Queries
{
    public class GetUserByIdQuery : IRequest<SingleUserQueryResponse>
    {
        public string UserId { get; }
        public GetUserByIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
