using DiscussionBoard.Authorization;
using Microsoft.AspNetCore.Identity;

namespace DiscussionBoard.Models
{
    public class DiscussionThreadUserManager
    {
        public DiscussionThread thread;
        public UserManager<ApplicationUser> userManager;
        public IApplicationAuthorizationService ApplicationAuthorizationService {get; set;}

        public DiscussionThreadUserManager(DiscussionThread thread, UserManager<ApplicationUser> userManager, IApplicationAuthorizationService applicationAuthorizationService)
        {
            this.thread = thread;
            this.userManager = userManager;
            ApplicationAuthorizationService = applicationAuthorizationService;
        }

    }
}
