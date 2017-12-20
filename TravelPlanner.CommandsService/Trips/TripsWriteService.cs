using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.CommandsServices.Trips.Commands;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.CommandsServices.Trips
{
    public class TripsWriteService : ITripsWriteService
    {
        private readonly IMediator _mediator;
        public TripsWriteService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TripCommandResponse> CreateTripAsync(TravelUser user,Trip trip)
        {
            var command = new CreateTripCommand(user,trip);
            return await _mediator.Send(command);
        }

        public async Task<TripCommandResponse> UpdateTripAsync(TravelUser user, Trip trip)
        {
            var command = new UpdateTripCommand(user, trip);
            return await _mediator.Send(command);
        }

        public async Task<TripCommandResponse> DeleteTripAsync(TravelUser user, int TripId)
        {
            var command = new DeleteTripCommand(user,TripId);
            return await _mediator.Send(command);
        }

    }
}
