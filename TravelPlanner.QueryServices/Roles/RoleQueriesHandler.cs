using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.QueryServices.Roles.Queries;
using System.Threading;
using System.Threading.Tasks;
using TravelPlanner.Shared.Entities;
using TravelPlanner.Shared.Enums;
using TravelPlanner.Shared.IRepos;
using TravelPlanner.Shared;

namespace TravelPlanner.QueryServices.Roles
{
    public class RoleQueriesHandler : BaseHandler,
        IRequestHandler<GetAllRolesQuery, MultipleRolesQueryResponse>,
        IRequestHandler<GetRoleByNameQuery, SingleRoleQueryResponse>,
        IRequestHandler<GetRoleByIdQuery, SingleRoleQueryResponse>
        
    {
        private readonly IRolesReadRepo _repo;

        public RoleQueriesHandler(IRolesReadRepo repo)
        {
            _repo = repo;
        }

        public Task<MultipleRolesQueryResponse> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            ICollection<UserRole> roles = _repo.GetAllRoles();
            var response = new MultipleRolesQueryResponse(roles);
            response.Status = GetResponseStatus(roles);
            return Task.FromResult(response);
        }

        public Task<SingleRoleQueryResponse> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
        {
            UserRole role = _repo.GetRoleByName(request.Name);
            var response = new SingleRoleQueryResponse(role);
            response.Status = GetResponseStatus(role);
            return Task.FromResult(response);
        }

        public Task<SingleRoleQueryResponse> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            UserRole role = _repo.GetRoleById(request.RoleId);
            var response = new SingleRoleQueryResponse(role);
            response.Status = GetResponseStatus(role);
            return Task.FromResult(response);
        }
    }
}
