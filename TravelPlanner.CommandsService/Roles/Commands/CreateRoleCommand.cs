using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.CommandsServices.Roles.Commands
{
    public class CreateRoleCommand : IRequest<RoleCommandResponse>
    {
        public UserRole Data { get; }

        public CreateRoleCommand(UserRole data)
        {
            Data = data;
        }
    }
}
