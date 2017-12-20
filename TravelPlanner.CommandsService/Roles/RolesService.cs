using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.CommandsServices.Roles.Commands;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.CommandsServices.Roles
{
    public class RolesService
    {
        private readonly IMediator _mediator;
        public RolesService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<RoleCommandResponse> CreateRoleAsync(UserRole role)
        {
            var command = new CreateRoleCommand(role);
            return await _mediator.Send(command);
        }

        public async Task<RoleCommandResponse> UpdateRoleAsync(UserRole role)
        {
            var command = new UpdateRoleCommand(role);
            return await _mediator.Send(command);
        }

        public async Task<RoleCommandResponse> DeleteRoleAsync(string roleId)
        {
            var command = new DeleteRoleCommand(roleId);
            return await _mediator.Send(command);
        }

    }
}
