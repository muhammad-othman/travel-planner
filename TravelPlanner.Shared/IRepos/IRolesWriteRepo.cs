using System;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.Shared.IRepos
{
    public interface IRolesWriteRepo
    {
        UserRole CreateRole(UserRole data);
        UserRole UpdateRole(UserRole data);
        UserRole DeleteRole(string userId);
    }
}
