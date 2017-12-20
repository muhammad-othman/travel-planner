using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.QueryServices.Trips.Queries
{
    public class GetTripByIdQuery : IRequest<SingleTripQueryResponse>
    {
        public TravelUser CurrentUser { get; }
        public int TripId { get; }
        public GetTripByIdQuery(TravelUser currentUser, int tripId)
        {
            CurrentUser = currentUser;
            TripId = tripId;
        }
    }
}
