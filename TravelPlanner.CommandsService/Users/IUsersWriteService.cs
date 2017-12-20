using System.Threading.Tasks;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.CommandsServices.Users
{
    public interface IUsersWriteService
    {
        Task<UserCommandResponse> CreateUserAsync(TravelUser user);
        Task<UserCommandResponse> DeleteUserAsync(string userId);
        Task<UserCommandResponse> UpdateUserAsync(TravelUser user);
    }
}