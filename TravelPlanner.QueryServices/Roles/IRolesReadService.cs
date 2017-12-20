using System.Threading.Tasks;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.QueryServices.Roles
{
    public interface IRolesReadService
    {
        Task<MultipleRolesQueryResponse> GetAllRoles(UserRole currentRole);
        Task<SingleRoleQueryResponse> GetRoleById(string id);
        Task<SingleRoleQueryResponse> GetRoleByName(string name);
    }
}