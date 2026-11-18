using System.ComponentModel.DataAnnotations;

namespace DiscussionBoard.Models
{
    public class DiscussionBase : EntityBase
    {
        [MinLength(3), MaxLength(160)]
        public string Title { get; set; }
        [MinLength(1), MaxLength(1000)]
        public string Content { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
