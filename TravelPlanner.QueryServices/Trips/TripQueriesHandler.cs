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
using System.Linq;

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

            int totalCount = trips.Count;
            if (request.PageIndex.HasValue && request.PageSize.HasValue)
                trips = trips.Skip((request.PageIndex.Value - 1) * request.PageSize.Value).Take(request.PageSize.Value).ToList();

            var response = new MultipleTripsQueryResponse(trips,totalCount);
            if (trips != null)
                response.Status = ResponseStatus.Succeeded;
            else
                response.Status = ResponseStatus.Failed;

            return Task.FromResult(response);
        }

        public Task<MultipleTripsQueryResponse> Handle(GetUserTripsQuery request, CancellationToken cancellationToken)
        {
            ICollection<Trip> trips = _repo.GetUserTrips(request.UserId, request.From, request.To, request.Destination);

            int totalCount = trips.Count;
            if (request.PageIndex.HasValue && request.PageSize.HasValue)
                trips = trips.Skip((request.PageIndex.Value - 1) * request.PageSize.Value).Take(request.PageSize.Value).ToList();

            var response = new MultipleTripsQueryResponse(trips,totalCount);
            if (trips != null)
                response.Status = ResponseStatus.Succeeded;
            else
                response.Status = ResponseStatus.Failed;

            return Task.FromResult(response);
        }

        public Task<SingleTripQueryResponse> Handle(GetTripByIdQuery request, CancellationToken cancellationToken)
        {
            Trip trip = _repo.GetTripById(request.TripId);
            var response = new SingleTripQueryResponse(trip);
            if (trip != null)
                response.Status = ResponseStatus.Succeeded;
            else
                response.Status = ResponseStatus.Failed;

            return Task.FromResult(response);
        }
    }
}
