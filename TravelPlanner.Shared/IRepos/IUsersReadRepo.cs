using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.Shared.IRepos
{
    public interface IUsersReadRepo
    {
        ICollection<TravelUser> GetAllUsers();
        TravelUser GetUserById(string userId);
        TravelUser GetUserByEmail(string email);
    }
}
