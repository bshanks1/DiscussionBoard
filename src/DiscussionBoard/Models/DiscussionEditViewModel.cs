using System.ComponentModel.DataAnnotations;

namespace DiscussionBoard.Models
{
    public class DiscussionEditViewModel
    {
        [MinLength(3), MaxLength(160)]
        public string Title { get; set; }
        [MinLength(1), MaxLength(1000)]
        public string Content { get; set; }
    }
}
