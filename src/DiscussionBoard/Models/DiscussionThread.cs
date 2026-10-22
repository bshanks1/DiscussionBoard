namespace DiscussionBoard.Models
{
    public class DiscussionThread : DiscussionBase
    {
        public Forum Forum { get; set; }
        public int ForumId { get; set; }
    }
}
