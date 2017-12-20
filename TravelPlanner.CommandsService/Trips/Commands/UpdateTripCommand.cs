using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.CommandsServices.Trips.Commands
{
    public class UpdateTripCommand : IRequest<TripCommandResponse>
    {
        public TravelUser CurrentUser { get; }
        public Trip Data { get; }

        public UpdateTripCommand(TravelUser currentUser, Trip data)
        {
            CurrentUser = currentUser;
            Data = data;
        }
    }
}