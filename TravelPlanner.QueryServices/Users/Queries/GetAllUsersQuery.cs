using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.QueryServices.Users.Queries
{
    public class GetAllUsersQuery : IRequest<MultipleUsersQueryResponse>
    {

        public TravelUser CurrentUser { get; }
        public string Email { get; }
        public int? PageIndex { get; }
        public int? PageSize { get; }

        public GetAllUsersQuery(TravelUser currentUser, string email, int? pageIndex, int? pageSize)
        {
            CurrentUser = currentUser;
            Email = email;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
