using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.QueryServices.Trips.Queries
{
    public class GetAllTripsQuery : IRequest<MultipleTripsQueryResponse>
    {
        public TravelUser CurrentUser { get; }
        public DateTime? From { get; }
        public DateTime? To { get; }
        public string Destination { get; }

        public GetAllTripsQuery(TravelUser currentUser, DateTime? from, DateTime? to, string destination)
        {
            CurrentUser = currentUser;
            From = from;
            To = to;
            Destination = destination;
        }
    }
}
