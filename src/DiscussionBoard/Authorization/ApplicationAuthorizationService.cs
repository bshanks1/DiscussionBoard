using System.Security.Claims;
using DiscussionBoard.Data;
using DiscussionBoard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace DiscussionBoard.Authorization
{
    public class ApplicationAuthorizationService : IApplicationAuthorizationService
    {
        public UserManager<ApplicationUser> UserManager;
        public IAuthorizationService AuthorizationService;

        public ApplicationAuthorizationService(UserManager<ApplicationUser> userManager, IAuthorizationService authorizationService)
        {
            UserManager = userManager;
            AuthorizationService = authorizationService;
        }
        public Task<bool> CanEditAsync(ClaimsPrincipal principal, DiscussionBase resource)
        {
            if (AuthorizationService.AuthorizeAsync(principal, ApplicationRoles.Admin).Result.Succeeded)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<IdentityResult> AddRoleAsync(ClaimsPrincipal actingUser, string targetUserId, string roleName)
        {
            if(actingUser.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == ApplicationRoles.Admin))
            {
                return Task.FromResult(UserManager.AddToRoleAsync(UserManager.FindByIdAsync(targetUserId).Result, roleName).Result);
            }

            return Task.FromResult(IdentityResult.Failed());
        }

        public Task<IdentityResult> RemoveRoleAsync(ClaimsPrincipal actingUser, string targetUserId, string roleName)
        {
            if(actingUser.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == ApplicationRoles.Admin))
            {
                if (!UserManager.GetUserAsync(actingUser).Result.Id.Equals(targetUserId) || UserManager.GetUsersInRoleAsync(ApplicationRoles.Admin).Result.Count > 1)
                {
                    return Task.FromResult(UserManager.RemoveFromRoleAsync(UserManager.FindByIdAsync(targetUserId).Result, roleName).Result);
                }
            }

            return Task.FromResult(IdentityResult.Failed());
        }
    }
}
