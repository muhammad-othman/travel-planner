using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelPlanner.QueryServices.Roles.Queries
{
    public class GetRoleByNameQuery : IRequest<SingleRoleQueryResponse>
    {
        public string Name { get; }
        public GetRoleByNameQuery(string name)
        {
            Name = name;
        }
    }
}
