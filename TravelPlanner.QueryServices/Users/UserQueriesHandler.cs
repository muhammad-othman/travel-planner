using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.QueryServices.Users.Queries;
using System.Threading;
using System.Threading.Tasks;
using TravelPlanner.Shared.Entities;
using TravelPlanner.Shared.Enums;
using TravelPlanner.Shared.IRepos;
using System.Linq;
using TravelPlanner.Shared;

namespace TravelPlanner.QueryServices.Users
{
    public class UserQueriesHandler : BaseHandler,
        IRequestHandler<GetAllUsersQuery, MultipleUsersQueryResponse>,
        IRequestHandler<GetUserByEmailQuery, SingleUserQueryResponse>,
        IRequestHandler<GetUserByIdQuery, SingleUserQueryResponse>,
        IRequestHandler<GetUserByQuery, SingleUserQueryResponse>
        
    {
        private readonly IUsersReadRepo _repo;

        public UserQueriesHandler(IUsersReadRepo repo)
        {
            _repo = repo;
        }

        public Task<MultipleUsersQueryResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            ICollection<TravelUser> users = _repo.GetAllUsers();
            int totalCount = users.Count;

            if(request.PageIndex.HasValue && request.PageSize.HasValue)
                users = users.Skip((request.PageIndex.Value- 1) * request.PageSize.Value).Take(request.PageSize.Value).ToList();

            var response = new MultipleUsersQueryResponse(users,totalCount);
            response.Status = GetResponseStatus(users);
            return Task.FromResult(response);
        }

        public Task<SingleUserQueryResponse> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            TravelUser user = _repo.GetUserByEmail(request.Email);
            var response = new SingleUserQueryResponse(user);
            response.Status = GetResponseStatus(user);
            return Task.FromResult(response);
        }

        public Task<SingleUserQueryResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            TravelUser user = _repo.GetUserById(request.UserId);
            var response = new SingleUserQueryResponse(user);
            response.Status = GetResponseStatus(user);
            return Task.FromResult(response);
        }

        public Task<SingleUserQueryResponse> Handle(GetUserByQuery request, CancellationToken cancellationToken)
        {
            TravelUser user = _repo.GetUserBy(request.Expression);
            var response = new SingleUserQueryResponse(user);
            response.Status = GetResponseStatus(user);
            return Task.FromResult(response);
        }
    }
}
