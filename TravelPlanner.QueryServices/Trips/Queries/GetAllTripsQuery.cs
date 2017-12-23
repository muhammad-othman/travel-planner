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
        public int? PageIndex { get; }
        public int? PageSize { get; }

        public GetAllTripsQuery(TravelUser currentUser, DateTime? from, DateTime? to, string destination, int? pageIndex, int? pageSize)
        {
            CurrentUser = currentUser;
            From = from;
            To = to;
            Destination = destination;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}