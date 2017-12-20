using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelPlanner.CommandsServices.Users.Commands
{
    public class DeleteUserCommand : IRequest<UserCommandResponse>
    {
        public string UserId { get; }

        public DeleteUserCommand(string userId)
        {
            UserId = userId;
        }
    }
}
