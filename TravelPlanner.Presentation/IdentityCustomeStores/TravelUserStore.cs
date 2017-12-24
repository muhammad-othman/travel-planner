using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using TravelPlanner.Shared.Entities;
using TravelPlanner.QueryServices.Users;
using TravelPlanner.CommandsServices.Users;
using TravelPlanner.Shared.Enums;

namespace TravelPlanner.Presentation.IdentityCustomeStores
{
    public class TravelUserStore : IUserStore<TravelUser>,
                         IUserLoginStore<TravelUser>,
                         IUserRoleStore<TravelUser>,
                         IUserPasswordStore<TravelUser>,
                         IUserSecurityStampStore<TravelUser>,
                         IUserEmailStore<TravelUser>
    {
        private readonly IUsersReadService _usersReadService;
        private readonly IUsersWriteService _usersWriteService;

        public TravelUserStore(IUsersReadService usersReadService, IUsersWriteService usersWriteService)
        {
            _usersReadService = usersReadService;
            _usersWriteService = usersWriteService;
        }
        

        public async Task<IdentityResult> CreateAsync(TravelUser user, CancellationToken cancellationToken)
        {
            var result =  await _usersWriteService.CreateUserAsync(user);
            if (result.Status == ResponseStatus.Succeeded)
                return IdentityResult.Success;
            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteAsync(TravelUser user, CancellationToken cancellationToken)
        {
            var result = await _usersWriteService.DeleteUserAsync(user.Id);
            if (result.Status == ResponseStatus.Succeeded)
                return IdentityResult.Success;
            return IdentityResult.Failed();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        
        public async Task<TravelUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var result = await _usersReadService.GetUserById(userId);
            if (result.Status == ResponseStatus.Succeeded)
                return result.User;
            return null;
        }

        

        public async Task<TravelUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var result = await _usersReadService.GetUserByEmail(normalizedUserName);
            if (result.Status == ResponseStatus.Succeeded)
                return result.User;
            return null;
        }

        
        public Task<string> GetNormalizedUserNameAsync(TravelUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<string> GetPasswordHashAsync(TravelUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<IList<string>> GetRolesAsync(TravelUser user, CancellationToken cancellationToken)
        {
            IList<string> roles = new List<string>();
            foreach (var role in user.Roles)
            {
                roles.Add(role.Name);
            }
            return Task.FromResult(roles);
        }

        public Task<string> GetSecurityStampAsync(TravelUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public Task<string> GetUserIdAsync(TravelUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(TravelUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<IList<TravelUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(TravelUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public Task<bool> IsInRoleAsync(TravelUser user, string roleName, CancellationToken cancellationToken)
        {
            bool isInRole = false;
            foreach (var role in user.Roles)
            {
                if (role.Name.ToLower() == roleName.ToLower())
                    isInRole = true;
            }
            return Task.FromResult(isInRole);
        }

        public Task RemoveFromRoleAsync(TravelUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task<IList<UserLoginInfo>> GetLoginsAsync(TravelUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task<TravelUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task AddLoginAsync(TravelUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(TravelUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task RemoveLoginAsync(TravelUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(TravelUser user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.Run(()=>user.Email = normalizedName);
        }

        public Task SetPasswordHashAsync(TravelUser user, string passwordHash, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.PasswordHash = passwordHash);
        }

        public Task SetSecurityStampAsync(TravelUser user, string stamp, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.SecurityStamp = stamp);
        }

        public Task SetUserNameAsync(TravelUser user, string userName, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.Email = userName);
        }

        public async Task<IdentityResult> UpdateAsync(TravelUser user, CancellationToken cancellationToken)
        {
            var result = await _usersWriteService.UpdateUserAsync(user);
            if (result.Status == ResponseStatus.Succeeded)
                return IdentityResult.Success;
            return IdentityResult.Failed();
        }

        public Task SetEmailAsync(TravelUser user, string email, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.Email = email);
        }

        public Task<string> GetEmailAsync(TravelUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(TravelUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(TravelUser user, bool confirmed, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.EmailConfirmed = confirmed);
        }

        public async Task<TravelUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            var result = await _usersReadService.GetUserByEmail(normalizedEmail);
            if (result.Status == ResponseStatus.Succeeded)
                return result.User;
            return null;
        }

        public Task<string> GetNormalizedEmailAsync(TravelUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(TravelUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.NormalizedEmail = normalizedEmail);
        }
    }
}
