using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.QueryServices.Trips.Queries
{
    public class GetUserTripsQuery : IRequest<MultipleTripsQueryResponse>
    {
        public TravelUser CurrentUser { get; }
        public string UserId { get; }
        public DateTime? From { get; }
        public DateTime? To { get; }
        public string Destination { get; }
        public int? PageIndex { get; }
        public int? PageSize { get; }

        public GetUserTripsQuery(TravelUser currentUser, string userId, DateTime? from, DateTime? to, string destination, int? pageIndex, int? pageSize)
        {
            CurrentUser = currentUser;
            UserId = userId;
            From = from;
            To = to;
            Destination = destination;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
