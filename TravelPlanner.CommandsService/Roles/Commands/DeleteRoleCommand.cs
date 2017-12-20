using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelPlanner.CommandsServices.Roles.Commands
{
    public class DeleteRoleCommand : IRequest<RoleCommandResponse>
    {
        public string RoleId { get; }

        public DeleteRoleCommand(string roleId)
        {
            RoleId = roleId;
        }
    }
}
