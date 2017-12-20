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

namespace TravelPlanner.QueryServices.Users
{
    public class UserQueriesHandler :
        IRequestHandler<GetAllUsersQuery, MultipleUsersQueryResponse>,
        IRequestHandler<GetUserByEmailQuery, SingleUserQueryResponse>,
        IRequestHandler<GetUserByIdQuery, SingleUserQueryResponse>
    {
        private readonly IUsersReadRepo _repo;

        public UserQueriesHandler(IUsersReadRepo repo)
        {
            _repo = repo;
        }

        public Task<MultipleUsersQueryResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            ICollection<TravelUser> users = _repo.GetAllUsers();
            var response = new MultipleUsersQueryResponse(users);
            if (users != null)
                response.Result = Result.Succeeded;
            else
                response.Result = Result.Failed;

            return Task.FromResult(response);
        }

        public Task<SingleUserQueryResponse> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            TravelUser user = _repo.GetUserByEmail(request.Email);
            var response = new SingleUserQueryResponse(user);
            if (user != null)
                response.Result = Result.Succeeded;
            else
                response.Result = Result.Failed;

            return Task.FromResult(response);
        }

        public Task<SingleUserQueryResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            TravelUser user = _repo.GetUserById(request.UserId);
            var response = new SingleUserQueryResponse(user);
            if (user != null)
                response.Result = Result.Succeeded;
            else
                response.Result = Result.Failed;

            return Task.FromResult(response);
        }
    }
}
