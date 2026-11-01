namespace DiscussionBoard.Models
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
    }
}
