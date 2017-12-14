using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TravelPlanner.Model.Entities;
using TravelPlanner.ViewModels;

namespace TravelPlanner.Model.Repositories.IRepositories
{
    public interface ITravelUserRepository
    {
        TravelUser GetById(string id);
        UserViewModel Update(string Id, UserViewModel user, UserManager<TravelUser> userManager);
        TravelUser Delete(string Id);
        IEnumerable<TravelUser> GetUsers(string email);
        TravelUser UpdatePricture(string picture, TravelUser user);
    }
}
