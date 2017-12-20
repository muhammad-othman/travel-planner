using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;
using TravelPlanner.Shared.Enums;

namespace TravelPlanner.CommandsServices.Trips
{
    public class TripCommandResponse
    {
        public Result Result { get; set; }

        public ICollection<string> Errors { get; set; }
        public Trip Trip { get; }

        public TripCommandResponse(Trip trip)
        {
            Errors = new List<string>();
            Trip = trip;
        }
    }
}
