using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;
using TravelPlanner.Shared.Enums;

namespace TravelPlanner.QueryServices.Trips
{
    public class SingleTripQueryResponse
    {
        public ResponseStatus Status { get; set; }
        public ICollection<string> Errors { get; set; }
        public Trip Trip { get; }

        public SingleTripQueryResponse(Trip trip)
        {
            Errors = new List<string>();
            Trip = trip;
        }
    }
}
