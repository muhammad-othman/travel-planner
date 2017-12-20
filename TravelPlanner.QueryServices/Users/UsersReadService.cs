using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.QueryServices.Users.Queries;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.QueryServices.Users
{
    public class UsersReadService : IUsersReadService
    {
        private readonly IMediator _mediator;
        public UsersReadService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<MultipleUsersQueryResponse> GetAllUsers(TravelUser currentUser)
        {
            var query = new GetAllUsersQuery(currentUser);
            return await _mediator.Send(query);
        }
        public async Task<SingleUserQueryResponse> GetUserById(string id)
        {
            var query = new GetUserByIdQuery(id);
            return await _mediator.Send(query);
        }
        public async Task<SingleUserQueryResponse> GetUserByEmail (string email)
        {
            var query = new GetUserByEmailQuery(email);
            return await _mediator.Send(query);
        }
    }
}
