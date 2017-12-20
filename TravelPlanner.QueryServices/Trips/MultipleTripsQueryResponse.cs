using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;
using TravelPlanner.Shared.Enums;

namespace TravelPlanner.QueryServices.Trips
{
    public class MultipleTripsQueryResponse
    {
        public Result Result { get; set; }
        public ICollection<string> Errors { get; set; }
        public ICollection<Trip> Trips { get; }

        public MultipleTripsQueryResponse(ICollection<Trip> trips)
        {
            Errors = new List<string>();
            Trips = trips;
        }
    }
}
