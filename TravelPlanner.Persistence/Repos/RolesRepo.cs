using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelPlanner.Shared.Entities;
using TravelPlanner.Shared.IRepos;

namespace TravelPlanner.Persistence.Repos
{
    public class RolesRepo : IRolesReadRepo, IRolesWriteRepo
    {
        private readonly TravelPlannerContext _context;
        public RolesRepo(TravelPlannerContext context)
        {
            _context = context;
        }
        public UserRole CreateRole(UserRole role)
        {
            role = _context.Roles.Add(role).Entity;
            _context.SaveChanges();
            return role;
        }

        public UserRole DeleteRole(string roleId)
        {
            var entity = _context.Roles.Find(roleId);
            if (entity != null)
            {
                _context.Roles.Remove(entity);
                _context.SaveChanges();
            }
            return entity;
        }

        public ICollection<UserRole> GetAllRoles()
        {
            return _context.Roles.ToList();
        }

        public UserRole GetRoleByName(string name)
        {
            return _context.Roles.Where(e => e.Name.ToLower() == name.ToLower()).FirstOrDefault();
        }

        public UserRole GetRoleById(string roleId)
        {
            return _context.Roles.Find(roleId);
        }

        public UserRole UpdateRole(UserRole role)
        {
            var oldEntity = _context.Roles.Find(role.Id);
            if (oldEntity == null)
                return null;

            _context.Entry(oldEntity).CurrentValues.SetValues(role);
            _context.SaveChanges();
            return role;
        }
    }
}
