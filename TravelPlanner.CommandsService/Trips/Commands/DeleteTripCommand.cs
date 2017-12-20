using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.CommandsServices.Trips.Commands
{
    public class DeleteTripCommand : IRequest<TripCommandResponse>
    {
        public TravelUser CurrentUser { get; }
        public int TripId { get; }

        public DeleteTripCommand(TravelUser currentUser,int tripId)
        {
            CurrentUser = currentUser;
            TripId = tripId;
        }
    }
}
