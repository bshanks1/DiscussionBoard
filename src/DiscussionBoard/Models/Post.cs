namespace DiscussionBoard.Models
{
    public class Post : DiscussionBase
    {
        public DiscussionThread Thread { get; set; }
        public int ThreadId { get; set; }
        public Post ParentPost { get; set; } = null!;
        public Post replyPost { get; set; }
    }
}
