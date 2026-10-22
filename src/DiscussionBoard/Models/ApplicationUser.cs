using Microsoft.AspNetCore.Identity;

namespace DiscussionBoard.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<DiscussionThread> DiscussionThreads = new List<DiscussionThread>();
        public List<Post> Posts = new List<Post>();
    }
}
