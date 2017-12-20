using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.CommandsServices.Users.Commands
{
    public class UpdateUserCommand : IRequest<UserCommandResponse>
    {
        public TravelUser Data { get; }

        public UpdateUserCommand(TravelUser data)
        {
            Data = data;
        }
    }
}
