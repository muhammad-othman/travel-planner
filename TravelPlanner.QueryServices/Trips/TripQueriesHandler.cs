using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.QueryServices.Trips.Queries;
using System.Threading;
using System.Threading.Tasks;
using TravelPlanner.Shared.Enums;
using TravelPlanner.Shared.Entities;
using TravelPlanner.Shared.IRepos;

namespace TravelPlanner.QueryServices.Trips
{
    public class TripQueriesHandler :
        IRequestHandler<GetAllTripsQuery, MultipleTripsQueryResponse>,
        IRequestHandler<GetUserTripsQuery, MultipleTripsQueryResponse>,
        IRequestHandler<GetTripByIdQuery, SingleTripQueryResponse>
    {
        private readonly ITripsReadRepo _repo;

        public TripQueriesHandler(ITripsReadRepo repo)
        {
            _repo = repo;
        }

        public Task<MultipleTripsQueryResponse> Handle(GetAllTripsQuery request, CancellationToken cancellationToken)
        {
            ICollection<Trip> trips = _repo.GetAllTrips(request.From, request.To, request.Destination);
            var response = new MultipleTripsQueryResponse(trips);
            if (trips != null)
                response.Result = Result.Succeeded;
            else
                response.Result = Result.Failed;

            return Task.FromResult(response);
        }

        public Task<MultipleTripsQueryResponse> Handle(GetUserTripsQuery request, CancellationToken cancellationToken)
        {
            ICollection<Trip> trips = _repo.GetUserTrips(request.UserId, request.From, request.To, request.Destination);
            var response = new MultipleTripsQueryResponse(trips);
            if (trips != null)
                response.Result = Result.Succeeded;
            else
                response.Result = Result.Failed;

            return Task.FromResult(response);
        }

        public Task<SingleTripQueryResponse> Handle(GetTripByIdQuery request, CancellationToken cancellationToken)
        {
            Trip trip = _repo.GetTripById(request.TripId);
            var response = new SingleTripQueryResponse(trip);
            if (trip != null)
                response.Result = Result.Succeeded;
            else
                response.Result = Result.Failed;

            return Task.FromResult(response);
        }
    }
}
