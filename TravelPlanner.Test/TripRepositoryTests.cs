using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelPlanner.Presentation.Model;
using TravelPlanner.Presentation.Model.Entities;
using TravelPlanner.Presentation.Model.Repositories;

namespace TravelPlanner.Test
{
    [TestClass]
    public class TripRepositoryTests
    {

        TripRepository tripsRepo;
        private Mock<DbSet<Trip>> mockTrips;

        IQueryable<Trip> data;
        [TestInitialize]
        public void TestInit()
        {
            var user1 = new TravelUser{ Id="user1ID", UserName = "user1@tp.com",NormalizedUserName = "user1@tp.com",Email = "user1@tp.com",NormalizedEmail = "user1@travelplanner.com",EmailConfirmed = true,LockoutEnabled = true,CreationDate = DateTime.Now};
            var user2 = new TravelUser{ Id = "user2ID", UserName = "user2@tp.com",NormalizedUserName = "user2@tp.com",Email = "user2@tp.com",NormalizedEmail = "user2@travelplanner.com",EmailConfirmed = true,LockoutEnabled = true,CreationDate = DateTime.Now};
            var user3 = new TravelUser{ Id = "user3ID", UserName = "user3@tp.com",NormalizedUserName = "user3@tp.com",Email = "user3@tp.com",NormalizedEmail = "user3@travelplanner.com",EmailConfirmed = true,LockoutEnabled = true,CreationDate = DateTime.Now};

            var trip1 = new Trip { Id = 1, Destination = "trip1Dist", Comment = "trip1Coment", EndDate = DateTime.Now.AddDays(20), StartDate = DateTime.Now.AddDays(10), SightseeingsCollection = "trip1Sights", TravelUser = user1, TravelUserId = user1.Id };
            var trip2 = new Trip { Id = 2, Destination = "trip2Dist", Comment = "trip2Coment", EndDate = DateTime.Now.AddDays(30), StartDate = DateTime.Now.AddDays(20), SightseeingsCollection = "trip2Sights", TravelUser = user1, TravelUserId = user1.Id };
            var trip3 = new Trip { Id = 3, Destination = "trip3Dist", Comment = "trip3Coment", EndDate = DateTime.Now.AddDays(40), StartDate = DateTime.Now.AddDays(30), SightseeingsCollection = "trip3Sights", TravelUser = user2, TravelUserId = user2.Id };
            var trip4 = new Trip { Id = 4, Destination = "trip4Dist", Comment = "trip4Coment", EndDate = DateTime.Now.AddDays(50), StartDate = DateTime.Now.AddDays(40), SightseeingsCollection = "trip4Sights", TravelUser = user2, TravelUserId = user2.Id };
            var trip5 = new Trip { Id = 5, Destination = "trip5Dist", Comment = "trip5Coment", EndDate = DateTime.Now.AddDays(60), StartDate = DateTime.Now.AddDays(50), SightseeingsCollection = "trip5Sights", TravelUser = user2, TravelUserId = user2.Id };
            var trip6 = new Trip { Id = 6, Destination = "trip6Dist", Comment = "trip6Coment", EndDate = DateTime.Now.AddDays(70), StartDate = DateTime.Now.AddDays(60), SightseeingsCollection = "trip6Sights", TravelUser = user2, TravelUserId = user2.Id };
            var trip7 = new Trip { Id = 7, Destination = "trip7Dist", Comment = "trip7Coment", EndDate = DateTime.Now.AddDays(80), StartDate = DateTime.Now.AddDays(40), SightseeingsCollection = "trip7Sights", TravelUser = user3, TravelUserId = user3.Id };
            var trip8 = new Trip { Id = 8, Destination = "trip8Dist", Comment = "trip8Coment", EndDate = DateTime.Now.AddDays(90), StartDate = DateTime.Now.AddDays(80), SightseeingsCollection = "trip8Sights", TravelUser = user3, TravelUserId = user3.Id };
            var trip9 = new Trip { Id = 9, Destination = "trip9Dist", Comment = "trip9Coment", EndDate = DateTime.Now.AddDays(15), StartDate = DateTime.Now.AddDays(11), SightseeingsCollection = "trip9Sights", TravelUser = user3, TravelUserId = user3.Id };

            user1.Trips.Add(trip1);
            user1.Trips.Add(trip2);

            user2.Trips.Add(trip3);
            user2.Trips.Add(trip4);
            user2.Trips.Add(trip5);
            user2.Trips.Add(trip6);

            user3.Trips.Add(trip7);
            user3.Trips.Add(trip8);
            user3.Trips.Add(trip9);

            data = new List<Trip>() {
                trip1,
                trip2,
                trip3,
                trip4,
                trip5,
                trip6,
                trip7,
                trip8,
                trip9}.AsQueryable();

            mockTrips = new Mock<DbSet<Trip>>();
            mockTrips.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(data.Provider);
            mockTrips.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(data.Expression);
            mockTrips.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockTrips.As<IQueryable<Trip>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockDbContext = new Mock<TravelPlannerContext>();
            mockDbContext.Setup(c => c.Trips).Returns(mockTrips.Object);

            tripsRepo = new TripRepository(mockDbContext.Object);
        }
        [TestMethod]
        public void GetAll_ShouldReturnAllTrips()
        {
            var trips = tripsRepo.GetTrips(null,null,null).ToList();
            var data = this.data.ToList();
            Assert.AreEqual(trips[0], data[0]);
            Assert.AreEqual(trips[1], data[1]);
            Assert.AreEqual(trips[2], data[2]);
        }

        [TestMethod]
        public void GetTripByID_ShouldReturnTheRightTrip()
        {
            var trip = tripsRepo.GetById(5);
            Assert.AreEqual(trip, data.ToList()[4]);
        }

        [TestMethod]
        public void DeleteTrip_ShouldReturnDeleted()
        {
            var trip = tripsRepo.GetById(5);
            var deleted = tripsRepo.Delete(5);

            Assert.AreEqual(trip, deleted);
        }
    }
}
