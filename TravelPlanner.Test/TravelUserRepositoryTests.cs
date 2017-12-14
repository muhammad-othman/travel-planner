using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelPlanner.Model;
using TravelPlanner.Model.Entities;
using TravelPlanner.Model.Repositories;

namespace TravelPlanner.Test
{
    [TestClass]
    public class TravelUserRepositoryTests
    {

        TravelUserRepository usersRepo;
        private Mock<DbSet<TravelUser>> mockUsers;
        [TestInitialize]
        public void TestInit()
        {
            var user1 = new TravelUser{ Id="user1ID", UserName = "user1@tp.com",NormalizedUserName = "user1@tp.com",Email = "user1@tp.com",NormalizedEmail = "user1@travelplanner.com",EmailConfirmed = true,LockoutEnabled = true,CreationDate = DateTime.Now};
            var user2 = new TravelUser{ Id = "user2ID", UserName = "user2@tp.com",NormalizedUserName = "user2@tp.com",Email = "user2@tp.com",NormalizedEmail = "user2@travelplanner.com",EmailConfirmed = true,LockoutEnabled = true,CreationDate = DateTime.Now};
            var user3 = new TravelUser{ Id = "user3ID", UserName = "user3@tp.com",NormalizedUserName = "user3@tp.com",Email = "user3@tp.com",NormalizedEmail = "user3@travelplanner.com",EmailConfirmed = true,LockoutEnabled = true,CreationDate = DateTime.Now};

            var trip1 = new Trip { Id = 1, Destination = "trip1Dist", Comment = "trip1Coment", EndDate = DateTime.Now.AddDays(20), StartDate = DateTime.Now.AddDays(10), SightseeingsCollection = "trip1Sights", TravelUser = user1, TravelUserId = user1.Id };
            var trip2 = new Trip { Id = 1, Destination = "trip2Dist", Comment = "trip2Coment", EndDate = DateTime.Now.AddDays(30), StartDate = DateTime.Now.AddDays(20), SightseeingsCollection = "trip2Sights", TravelUser = user1, TravelUserId = user1.Id };
            var trip3 = new Trip { Id = 1, Destination = "trip3Dist", Comment = "trip3Coment", EndDate = DateTime.Now.AddDays(40), StartDate = DateTime.Now.AddDays(30), SightseeingsCollection = "trip3Sights", TravelUser = user2, TravelUserId = user2.Id };
            var trip4 = new Trip { Id = 1, Destination = "trip4Dist", Comment = "trip4Coment", EndDate = DateTime.Now.AddDays(50), StartDate = DateTime.Now.AddDays(40), SightseeingsCollection = "trip4Sights", TravelUser = user2, TravelUserId = user2.Id };
            var trip5 = new Trip { Id = 1, Destination = "trip5Dist", Comment = "trip5Coment", EndDate = DateTime.Now.AddDays(60), StartDate = DateTime.Now.AddDays(50), SightseeingsCollection = "trip5Sights", TravelUser = user2, TravelUserId = user2.Id };
            var trip6 = new Trip { Id = 1, Destination = "trip6Dist", Comment = "trip6Coment", EndDate = DateTime.Now.AddDays(70), StartDate = DateTime.Now.AddDays(60), SightseeingsCollection = "trip6Sights", TravelUser = user2, TravelUserId = user2.Id };
            var trip7 = new Trip { Id = 1, Destination = "trip7Dist", Comment = "trip7Coment", EndDate = DateTime.Now.AddDays(80), StartDate = DateTime.Now.AddDays(40), SightseeingsCollection = "trip7Sights", TravelUser = user3, TravelUserId = user3.Id };
            var trip8 = new Trip { Id = 1, Destination = "trip8Dist", Comment = "trip8Coment", EndDate = DateTime.Now.AddDays(90), StartDate = DateTime.Now.AddDays(80), SightseeingsCollection = "trip8Sights", TravelUser = user3, TravelUserId = user3.Id };
            var trip9 = new Trip { Id = 1, Destination = "trip9Dist", Comment = "trip9Coment", EndDate = DateTime.Now.AddDays(15), StartDate = DateTime.Now.AddDays(11), SightseeingsCollection = "trip9Sights", TravelUser = user3, TravelUserId = user3.Id };

            user1.Trips.Add(trip1);
            user1.Trips.Add(trip2);

            user2.Trips.Add(trip3);
            user2.Trips.Add(trip4);
            user2.Trips.Add(trip5);
            user2.Trips.Add(trip6);

            user3.Trips.Add(trip7);
            user3.Trips.Add(trip8);
            user3.Trips.Add(trip9);

            var data = new List<TravelUser>() { user1,user2,user3}.AsQueryable();

            mockUsers = new Mock<DbSet<TravelUser>>();
            mockUsers.As<IQueryable<TravelUser>>().Setup(m => m.Provider).Returns(data.Provider);
            mockUsers.As<IQueryable<TravelUser>>().Setup(m => m.Expression).Returns(data.Expression);
            mockUsers.As<IQueryable<TravelUser>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockUsers.As<IQueryable<TravelUser>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockDbContext = new Mock<TravelPlannerContext>();
            mockDbContext.Setup(c => c.TravelUsers).Returns(mockUsers.Object);

            usersRepo = new TravelUserRepository(mockDbContext.Object);
        }

        [TestMethod]
        public void CountUserTrips_ShouldBeCorrect()
        {
            var u = usersRepo.GetById("user1ID");
            
            Assert.AreEqual(u.Trips.Count, 2);
        }

        [TestMethod]
        public void GetUserByMail_ShouldBeTheSameAsGetByID()
        {
            var u = usersRepo.GetById("user1ID");
            var u2 = usersRepo.GetUsers("user1").First();

            Assert.AreEqual(u, u2);
        }

        [TestMethod]
        public void DeleteUser_ShouldReturnDeleted()
        {
            var user = usersRepo.GetById("user1ID");
            var deleted = usersRepo.Delete("user1ID");


            Assert.AreEqual(user, deleted);
        }
    }
}
