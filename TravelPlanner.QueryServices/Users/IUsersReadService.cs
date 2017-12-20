using System.Threading.Tasks;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.QueryServices.Users
{
    public interface IUsersReadService
    {
        Task<MultipleUsersQueryResponse> GetAllUsers(TravelUser currentUser, string email);
        Task<SingleUserQueryResponse> GetUserByEmail(string email);
        Task<SingleUserQueryResponse> GetUserById(string id);
    }
}