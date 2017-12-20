using System;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.Shared.IRepos
{
    public interface IUsersWriteRepo
    {
        TravelUser CreateUser(TravelUser data);
        TravelUser UpdateUser(TravelUser data);
        TravelUser DeleteUser(string userId);
    }
}
