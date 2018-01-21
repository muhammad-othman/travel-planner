﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.QueryServices.Users.Queries;
using TravelPlanner.Shared.Entities;
using System.Linq.Expressions;

namespace TravelPlanner.QueryServices.Users
{
    public class UsersReadService : IUsersReadService
    {
        private readonly IMediator _mediator;
        public UsersReadService(IMediator mediator)
        {
            _mediator = mediator;
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
        public async Task<MultipleUsersQueryResponse> GetAllUsersAsync(TravelUser currentUser, string email, int? pageIndex, int? pageSize)
        {
            var query = new GetAllUsersQuery(currentUser,email,pageIndex, pageSize);
            return await _mediator.Send(query);
        }

        public async Task<SingleUserQueryResponse> GetUserBy(Expression<Func<TravelUser, bool>> expression)
        {
            var query = new GetUserByQuery(expression);
            return await _mediator.Send(query);
        }
    }
}
