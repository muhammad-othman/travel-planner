using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.QueryServices.Users.Queries
{
    public class GetUserByQuery : IRequest<SingleUserQueryResponse>
    {
        public GetUserByQuery(Expression<Func<TravelUser, bool>> expression)
        {
            Expression = expression;
        }

        public Expression<Func<TravelUser, bool>> Expression { get; }
    }
}
