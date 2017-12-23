using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.QueryServices.Trips.Queries;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.QueryServices.Trips
{
    public class TripsReadService : ITripsReadService
    {
        private readonly IMediator _mediator;
        public TripsReadService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<MultipleTripsQueryResponse> GetUserTrips(TravelUser currentUser, string userId, DateTime? from, DateTime? to, string destination, int? pageIndex, int? pageSize)
        {
            var query = new GetUserTripsQuery(currentUser, userId, from, to , destination, pageIndex, pageSize);
            return await _mediator.Send(query);
        }
        public async Task<MultipleTripsQueryResponse> GetAllTrips(TravelUser currentUser, DateTime? from, DateTime? to, string destination, int? pageIndex, int? pageSize)
        {
            var query = new GetAllTripsQuery(currentUser, from, to, destination, pageIndex, pageSize);
            return await _mediator.Send(query);
        }
        public async Task<SingleTripQueryResponse> GetTripById(TravelUser user, int TripId)
        {
            var query = new GetTripByIdQuery(user, TripId);
            return await _mediator.Send(query);
        }
    }
}
