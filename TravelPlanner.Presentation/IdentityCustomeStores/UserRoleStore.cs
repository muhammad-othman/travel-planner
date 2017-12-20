using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using TravelPlanner.Shared.Entities;
using TravelPlanner.QueryServices.Roles;
using TravelPlanner.CommandsServices.Roles;
using TravelPlanner.Shared.Enums;

namespace TravelPlanner.Presentation.IdentityCustomeStores
{
    public class UserRoleStore : IRoleStore<UserRole>
    {

        private readonly IRolesReadService _rolesReadService;
        private readonly IRolesWriteService _rolesWriteService;

        public UserRoleStore(IRolesReadService rolesReadService, IRolesWriteService rolesWriteService)
        {
            _rolesReadService = rolesReadService;
            _rolesWriteService = rolesWriteService;
        }


        public async Task<IdentityResult> CreateAsync(UserRole role, CancellationToken cancellationToken)
        {
            var result = await _rolesWriteService.CreateRoleAsync(role);
            if (result.Result == Result.Succeeded)
                return IdentityResult.Success;
            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteAsync(UserRole role, CancellationToken cancellationToken)
        {
            var result = await _rolesWriteService.DeleteRoleAsync(role.Id);
            if (result.Result == Result.Succeeded)
                return IdentityResult.Success;
            return IdentityResult.Failed();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }


        public async Task<UserRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            var result = await _rolesReadService.GetRoleById(roleId);
            if (result.Result == Result.Succeeded)
                return result.Role;
            return null;
        }



        public async Task<UserRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            var result = await _rolesReadService.GetRoleByName(normalizedRoleName);
            if (result.Result == Result.Succeeded)
                return result.Role;
            return null;
        }

        public Task<string> GetNormalizedRoleNameAsync(UserRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(UserRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(UserRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(UserRole role, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(UserRole role, string roleName, CancellationToken cancellationToken)
        {
            return Task.Run(() => role.Name = roleName);
        }

        public async Task<IdentityResult> UpdateAsync(UserRole role, CancellationToken cancellationToken)
        {
            var result = await _rolesWriteService.UpdateRoleAsync(role);
            if (result.Result == Result.Succeeded)
                return IdentityResult.Success;
            return IdentityResult.Failed();
        }
    }
}
