using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.CommandsServices.Users.Commands
{
    public class CreateUserCommand : IRequest<UserCommandResponse>
    {
        public TravelUser Data { get; }

        public CreateUserCommand(TravelUser data)
        {
            Data = data;
        }
    }
}
