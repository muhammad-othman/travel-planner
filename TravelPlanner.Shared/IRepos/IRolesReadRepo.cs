using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.Shared.IRepos
{
    public interface IRolesReadRepo
    {
        ICollection<UserRole> GetAllRoles();
        UserRole GetRoleById(string roleId);
        UserRole GetRoleByName(string name);
    }
}
