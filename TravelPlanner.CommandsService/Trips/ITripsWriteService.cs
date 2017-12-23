using System.Threading.Tasks;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.CommandsServices.Trips
{
    public interface ITripsWriteService
    {
        Task<TripCommandResponse> CreateTripAsync(TravelUser user, Trip trip);
        Task<TripCommandResponse> DeleteTripAsync(TravelUser user, int tripId);
        Task<TripCommandResponse> UpdateTripAsync(TravelUser user, Trip trip);
    }
}