using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.Shared.IRepos
{
    public interface ITripsReadRepo
    {
        Trip GetTripById(int tripId);
        ICollection<Trip> GetUserTrips(string userId, DateTime? from, DateTime? to, string destination);
        ICollection<Trip> GetAllTrips(DateTime? from, DateTime? to, string destination);
    }
}
