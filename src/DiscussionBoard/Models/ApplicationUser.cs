using Microsoft.AspNetCore.Identity;

namespace DiscussionBoard.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<DiscussionThread> DiscussionThreads { get; set; } = new List<DiscussionThread>();
        public List<Post> Posts { get; set; } = new List<Post>();

        public string DisplayName { get; set; } = " "
        public string AvatarId { get; set; } = "default";
    }
}
