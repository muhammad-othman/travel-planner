using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelPlanner.Shared.Entities;
using TravelPlanner.Shared.IRepos;

namespace TravelPlanner.Persistence.Repos
{
    public class TripsRepo : ITripsReadRepo, ITripsWriteRepo
    {
        private readonly TravelPlannerContext _context;
        public TripsRepo(TravelPlannerContext context)
        {
            _context = context;
        }
        public Trip CreateTrip(Trip trip)
        {
            trip = _context.Trips.Add(trip).Entity;
            _context.SaveChanges();
            return trip;
        }

        public Trip DeleteTrip(int tripId)
        {
            var entity = _context.Trips.Find(tripId);
            if (entity != null)
            {
                _context.Trips.Remove(entity);
                _context.SaveChanges();
            }
            return entity;
        }
        public ICollection<Trip> GetUserTrips(string userId, DateTime? from, DateTime? to, string destination)
        {
            if (string.IsNullOrWhiteSpace(destination))
                destination = string.Empty;
            return _context.Trips
                .Where(e => e.Destination.ToLower().Contains(destination.ToLower().Trim()) && FilterTripsDates(e, from, to) && e.TravelUserId == userId)
                   .Include(e => e.TravelUser).ToList();
        }
        public ICollection<Trip> GetAllTrips(DateTime? from, DateTime? to, string destination)
        {
            if (string.IsNullOrWhiteSpace(destination))
                destination = string.Empty;
            return _context.Trips.Where(e => e.Destination.Contains(destination.Trim()) && FilterTripsDates(e, from, to))
                   .Include(e => e.TravelUser).ToList();
        }
        private bool FilterTripsDates(Trip e, DateTime? from, DateTime? to)
        {
            if (from == null)
                from = DateTime.Now.AddYears(-200);
            if (to == null)
                to = DateTime.Now.AddYears(200);
            if (from <= e.StartDate && e.StartDate <= to)
                return true;
            if (from <= e.EndDate && e.EndDate <= to)
                return true;
            return false;
        }
        public Trip GetTripById(int tripId)
        {
            return _context.Trips.Find(tripId);
        }
        public Trip UpdateTrip(Trip trip)
        {
            var oldEntity = _context.Trips.Find(trip.Id);
            if (oldEntity == null)
                return null;

            trip.TravelUserId = oldEntity.TravelUserId;
            trip.TravelUser = oldEntity.TravelUser;
            _context.Entry(oldEntity).CurrentValues.SetValues(trip);
            _context.SaveChanges();
            trip.Id = oldEntity.Id;
            return trip;
        }
    }
}
