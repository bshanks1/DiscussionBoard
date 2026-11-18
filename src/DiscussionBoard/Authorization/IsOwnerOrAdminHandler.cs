using DiscussionBoard.Models;
using Microsoft.AspNetCore.Authorization;

namespace DiscussionBoard.Authorization
{
    public class IsOwnerOrAdminHandler : AuthorizationHandler<IsOwnerOrAdminRequirement, DiscussionBase>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            IsOwnerOrAdminRequirement requirement, DiscussionBase resource)
        {
            if (context.User != null && context.User.Identity.IsAuthenticated && context.User.Claims.ToList().Count > 0
                && context.User.Claims.ToList()[0].Value != null && context.User.IsInRole(ApplicationRoles.Admin) || context.User.Claims.ToList().Count > 0 && resource.ApplicationUserId == context.User.Claims.ToList()[0].Value)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
