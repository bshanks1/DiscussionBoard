namespace DiscussionBoard.Models
{
    public class DiscussionThread : DiscussionBase
    {
        public int ForumId { get; set; }
        public Forum Forum { get; set; }

        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
