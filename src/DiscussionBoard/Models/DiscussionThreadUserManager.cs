using Microsoft.AspNetCore.Identity;

namespace DiscussionBoard.Models
{
    public class DiscussionThreadUserManager
    {
        public DiscussionThread thread;
        public UserManager<ApplicationUser> userManager;

        public DiscussionThreadUserManager(DiscussionThread thread, UserManager<ApplicationUser> userManager)
        {
            this.thread = thread;
            this.userManager = userManager;
        }

    }
}
