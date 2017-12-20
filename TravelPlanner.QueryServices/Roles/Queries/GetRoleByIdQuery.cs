using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelPlanner.QueryServices.Roles.Queries
{
    public class GetRoleByIdQuery : IRequest<SingleRoleQueryResponse>
    {
        public string RoleId { get; }
        public GetRoleByIdQuery(string roleId)
        {
            RoleId = roleId;
        }
    }
}
