using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TravelPlanner.CommandsServices.Users;
using TravelPlanner.CommandsServices.Users.Commands;
using TravelPlanner.Shared.Entities;
using TravelPlanner.Shared.Enums;
using TravelPlanner.Shared.IRepos;

namespace TravelPlanner.CommandsServices.Users
{
    public class UserCommandsHandler : 
        IRequestHandler<CreateUserCommand, UserCommandResponse>, 
        IRequestHandler<DeleteUserCommand, UserCommandResponse>, 
        IRequestHandler<UpdateUserCommand, UserCommandResponse>
    {
        private readonly IUsersWriteRepo _repo;

        public UserCommandsHandler(IUsersWriteRepo repo)
        {
            _repo = repo;
        }
        public Task<UserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            TravelUser user = _repo.CreateUser(request.Data);
            var response = new UserCommandResponse(user);
            if (user != null)
                response.Result = Result.Succeeded;
            else
                response.Result = Result.Failed;

            return Task.FromResult(response);
        }

        public Task<UserCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            TravelUser user = _repo.UpdateUser(request.Data);
            var response = new UserCommandResponse(user);
            if (user != null)
                response.Result = Result.Succeeded;
            else
                response.Result = Result.Failed;

            return Task.FromResult(response);
        }

        public Task<UserCommandResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            TravelUser user = _repo.DeleteUser(request.UserId);
            var response = new UserCommandResponse(user);
            if (user != null)
                response.Result = Result.Succeeded;
            else
                response.Result = Result.Failed;

            return Task.FromResult(response);
        }
    }
}
