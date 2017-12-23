using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TravelPlanner.CommandsServices.Roles;
using TravelPlanner.CommandsServices.Roles.Commands;
using TravelPlanner.Shared.Entities;
using TravelPlanner.Shared.Enums;
using TravelPlanner.Shared.IRepos;

namespace TravelPlanner.CommandsServices.Roles
{
    public class RoleCommandsHandler : 
        IRequestHandler<CreateRoleCommand, RoleCommandResponse>, 
        IRequestHandler<DeleteRoleCommand, RoleCommandResponse>, 
        IRequestHandler<UpdateRoleCommand, RoleCommandResponse>
    {
        private readonly IRolesWriteRepo _repo;

        public RoleCommandsHandler(IRolesWriteRepo repo)
        {
            _repo = repo;
        }
        public Task<RoleCommandResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            UserRole role = _repo.CreateRole(request.Data);
            var response = new RoleCommandResponse(role);
            if (role != null)
                response.Status = ResponseStatus.Succeeded;
            else
                response.Status = ResponseStatus.Failed;

            return Task.FromResult(response);
        }

        public Task<RoleCommandResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            UserRole role = _repo.UpdateRole(request.Data);
            var response = new RoleCommandResponse(role);
            if (role != null)
                response.Status = ResponseStatus.Succeeded;
            else
                response.Status = ResponseStatus.Failed;

            return Task.FromResult(response);
        }

        public Task<RoleCommandResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            UserRole role = _repo.DeleteRole(request.RoleId);
            var response = new RoleCommandResponse(role);
            if (role != null)
                response.Status = ResponseStatus.Succeeded;
            else
                response.Status = ResponseStatus.Failed;

            return Task.FromResult(response);
        }
    }
}
