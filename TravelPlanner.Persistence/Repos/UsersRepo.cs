using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelPlanner.Shared.Entities;
using TravelPlanner.Shared.IRepos;

namespace TravelPlanner.Persistence.Repos
{
    public class UsersRepo : IUsersReadRepo, IUsersWriteRepo
    {
        private readonly TravelPlannerContext _context;
        public UsersRepo(TravelPlannerContext context)
        {
            _context = context;
        }
        public TravelUser CreateUser(TravelUser user)
        {
            user = _context.Users.Add(user).Entity;
            _context.SaveChanges();
            return user;
        }

        public TravelUser DeleteUser(string userId)
        {
            var entity = _context.Users.Find(userId);
            if (entity != null)
            {
                _context.Users.Remove(entity);
                _context.SaveChanges();
            }
            return entity;
        }

        public ICollection<TravelUser> GetAllUsers()
        {
            return _context.Users.Include(e=>e.Roles).ToList();
        }

        public TravelUser GetUserByEmail(string email)
        {
            return _context.Users.Where(e => e.Email.ToLower() == email.ToLower()).Include(e => e.Roles).FirstOrDefault();
        }

        public TravelUser GetUserById(string userId)
        {
            return _context.Users.Find(userId);
        }

        public TravelUser UpdateUser(TravelUser user)
        {
            var oldEntity = _context.Users.Find(user.Id);
            if (oldEntity == null)
                return null;
            
            _context.Entry(oldEntity).CurrentValues.SetValues(user);
            _context.SaveChanges();
            return user;
        }
    }
}
