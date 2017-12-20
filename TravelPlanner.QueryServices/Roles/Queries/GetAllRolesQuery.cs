using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.QueryServices.Roles.Queries
{
    public class GetAllRolesQuery : IRequest<MultipleRolesQueryResponse>
    {
        public UserRole CurrentRole { get; }
        public GetAllRolesQuery(UserRole currentRole)
        {
            CurrentRole = currentRole;
        }
    }
}
