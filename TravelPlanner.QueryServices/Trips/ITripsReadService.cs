using System;
using System.Threading.Tasks;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.QueryServices.Trips
{
    public interface ITripsReadService
    {
        Task<MultipleTripsQueryResponse> GetAllTrips(TravelUser currentUser, DateTime? from, DateTime? to, string destination, int? pageIndex, int? pageSize);
        Task<SingleTripQueryResponse> GetTripById(TravelUser user, int TripId);
        Task<MultipleTripsQueryResponse> GetUserTrips(TravelUser currentUser, string userId, DateTime? from, DateTime? to, string destination, int? pageIndex, int? pageSize);
    }
}