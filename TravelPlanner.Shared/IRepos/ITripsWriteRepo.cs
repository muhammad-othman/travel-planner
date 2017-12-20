using System;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.Shared.IRepos
{
    public interface ITripsWriteRepo
    {
        Trip CreateTrip(Trip data);
        Trip UpdateTrip(Trip data);
        Trip DeleteTrip(int tripId);
    }
}
