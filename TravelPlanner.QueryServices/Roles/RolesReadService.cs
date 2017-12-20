using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.QueryServices.Roles.Queries;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.QueryServices.Roles
{
    public class RolesReadService
    {
        private readonly IMediator _mediator;
        public RolesReadService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<MultipleRolesQueryResponse> GetAllRoles(UserRole currentRole)
        {
            var query = new GetAllRolesQuery(currentRole);
            return await _mediator.Send(query);
        }
        public async Task<SingleRoleQueryResponse> GetRoleById(string id)
        {
            var query = new GetRoleByIdQuery(id);
            return await _mediator.Send(query);
        }
        public async Task<SingleRoleQueryResponse> GetRoleByName (string name)
        {
            var query = new GetRoleByNameQuery(name);
            return await _mediator.Send(query);
        }
    }
}
