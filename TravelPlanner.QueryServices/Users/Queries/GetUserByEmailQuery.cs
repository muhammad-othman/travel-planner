using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelPlanner.QueryServices.Users.Queries
{
    public class GetUserByEmailQuery : IRequest<SingleUserQueryResponse>
    {
        public string Email { get; }
        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }
    }
}
