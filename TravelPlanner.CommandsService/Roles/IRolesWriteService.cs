using System.Threading.Tasks;
using TravelPlanner.Shared.Entities;

namespace TravelPlanner.CommandsServices.Roles
{
    public interface IRolesWriteService
    {
        Task<RoleCommandResponse> CreateRoleAsync(UserRole role);
        Task<RoleCommandResponse> DeleteRoleAsync(string roleId);
        Task<RoleCommandResponse> UpdateRoleAsync(UserRole role);
    }
}