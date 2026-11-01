namespace DiscussionBoard.Models
{
    public class Forum : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<DiscussionThread> DiscussionThreads { get; set; } = new List<DiscussionThread>();
    }
}
