using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;
using TravelPlanner.Shared.Enums;

namespace TravelPlanner.QueryServices.Trips
{
    public class MultipleTripsQueryResponse
    {
        public ResponseStatus Status { get; set; }
        public ICollection<string> Errors { get; set; }
        public ICollection<Trip> Trips { get; }
        public int TotalCount { get; set; }

        public MultipleTripsQueryResponse(ICollection<Trip> trips, int totalCount)
        {
            TotalCount = totalCount;
            Errors = new List<string>();
            Trips = trips;
        }
    }
}
