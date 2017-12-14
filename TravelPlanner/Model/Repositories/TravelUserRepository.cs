using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPlanner.Model.Entities;
using TravelPlanner.Model.Repositories.IRepositories;
using TravelPlanner.ViewModels;

namespace TravelPlanner.Model.Repositories
{
    public class TravelUserRepository : ITravelUserRepository
    {
        private readonly TravelPlannerContext _context;

        public TravelUserRepository(TravelPlannerContext context)
        {
            _context = context;
        }
        public TravelUser Delete(string Id)
        {
            var entity = _context.TravelUsers.SingleOrDefault(e => e.Id == Id);
            if (entity != null)
            {
                _context.TravelUsers.Remove(entity);
                _context.SaveChanges();
            }
            return entity;
        }

        public TravelUser GetById(string id)
        {
            return _context.TravelUsers.SingleOrDefault(e => e.Id == id);
        }

        public IEnumerable<TravelUser> GetUsers(string email)
        {
            email = email ?? string.Empty;
            return _context.TravelUsers.Where(e => e.Email.Contains(email)).ToList();
        }

        public UserViewModel Update(string Id, UserViewModel user, UserManager<TravelUser> userManager)
        {
            var oldEntity = _context.TravelUsers.Find(Id);
            if (oldEntity == null)
                return null;
            oldEntity.EmailConfirmed = user.EmailConfirmed;
            if (user.isLocked)
                oldEntity.LockoutEnd = DateTime.Now.AddYears(100);
            else
                oldEntity.LockoutEnd = null;

            if (user.Role == "admin")
                userManager.AddToRoleAsync(oldEntity, "admin").Wait();
            else
            {
                userManager.RemoveFromRoleAsync(oldEntity, "admin").Wait();
                if (user.Role == "manager")
                    userManager.AddToRoleAsync(oldEntity, "manager").Wait();
                else
                    userManager.RemoveFromRoleAsync(oldEntity, "manager").Wait();

            }
            _context.SaveChanges();
            return user;
        }

        public TravelUser UpdatePricture(string picture, TravelUser user)
        {
            user.Picture = picture;
            _context.SaveChanges();
            return user;
        }
    }
}
