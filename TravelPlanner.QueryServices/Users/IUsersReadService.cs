using System.Threading.Tasks;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.QueryServices.Users
{
    public interface IUsersReadService
    {
        Task<MultipleUsersQueryResponse> GetAllUsersAsync(TravelUser currentUser, string email, int? pageIndex, int? pageSize);
        Task<SingleUserQueryResponse> GetUserByEmail(string email);
        Task<SingleUserQueryResponse> GetUserById(string id);
    }
}