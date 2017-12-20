using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.CommandsServices.Roles.Commands
{
    public class UpdateRoleCommand : IRequest<RoleCommandResponse>
    {
        public UserRole Data { get; }

        public UpdateRoleCommand(UserRole data)
        {
            Data = data;
        }
    }
}
