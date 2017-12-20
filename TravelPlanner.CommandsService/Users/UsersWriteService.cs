using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.CommandsServices.Users.Commands;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.CommandsServices.Users
{
    public class UsersWriteService : IUsersWriteService
    {
        private readonly IMediator _mediator;
        public UsersWriteService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<UserCommandResponse> CreateUserAsync(TravelUser user)
        {
            var command = new CreateUserCommand(user);
            return await _mediator.Send(command);
        }

        public async Task<UserCommandResponse> UpdateUserAsync(TravelUser user)
        {
            var command = new UpdateUserCommand(user);
            return await _mediator.Send(command);
        }

        public async Task<UserCommandResponse> DeleteUserAsync(string userId)
        {
            var command = new DeleteUserCommand(userId);
            return await _mediator.Send(command);
        }

    }
}
