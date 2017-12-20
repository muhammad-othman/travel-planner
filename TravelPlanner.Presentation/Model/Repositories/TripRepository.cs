using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPlanner.Presentation.Model.Entities;
using TravelPlanner.Presentation.Model.Repositories.IRepositories;

namespace TravelPlanner.Presentation.Model.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly TravelPlannerContext _context;

        public TripRepository(TravelPlannerContext context)
        {
            _context = context;
        }
        public Trip Create(Trip trip)
        {
            trip = _context.Trips.Add(trip).Entity;
            _context.SaveChanges();
            return trip;
        }

        public Trip Delete(int Id)
        {
            var entity = _context.Trips.SingleOrDefault(e => e.Id == Id);
            if (entity != null)
            { 
                _context.Trips.Remove(entity);
                _context.SaveChanges();
            }
            return entity;
        }

        public IEnumerable<Trip> GetTrips(DateTime? from, DateTime? to, string destination)
        {
            if (string.IsNullOrWhiteSpace(destination))
                destination = string.Empty;
            return _context.Trips.Where(e => e.Destination.Contains(destination.Trim()) && FilterTripsDates(e,from,to))
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

        public IEnumerable<Trip> GetUserTrips(string userId, DateTime? from, DateTime? to, string destination)
        {
            if (string.IsNullOrWhiteSpace(destination))
                destination = string.Empty;
            return _context.Trips
                .Where(e => e.Destination.ToLower().Contains(destination.ToLower().Trim()) && FilterTripsDates(e, from, to) && e.TravelUserId == userId)
                   .Include(e => e.TravelUser).ToList();
        }

        public Trip Update(int Id, Trip trip)
        {
            var oldEntity = _context.Trips.Find(Id);
            if (oldEntity == null)
                return null;

            trip.Id= oldEntity.Id;
            trip.TravelUserId = oldEntity.TravelUserId;
            trip.TravelUser = oldEntity.TravelUser;
            _context.Entry(oldEntity).CurrentValues.SetValues(trip);
            _context.SaveChanges();
            trip.Id = oldEntity.Id;
            return trip;
        }

        public Trip GetById(int id)
        {
            return _context.Trips.SingleOrDefault(e => e.Id == id);
        }
    }
}
