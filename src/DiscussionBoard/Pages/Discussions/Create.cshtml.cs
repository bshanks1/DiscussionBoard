using System.Security.Claims;
using DiscussionBoard.Models;
using DiscussionBoard.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiscussionBoard.Pages.Discussions
{
    public class Create : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }
        [BindProperty (SupportsGet = true)]
        public int forumId { get; set; }
        public DiscussionThread Thread { get; set; }
        private readonly ILogger _logger;
        private readonly IDiscussionThreadService ThreadService;

        public Create(IDiscussionThreadService _threadService, ILogger<Create> logger)
        {
            _logger = logger;
            ThreadService = _threadService;
            Input = new InputModel();
        }

        public void OnPost()
        {
            if (Input.Title.Length > 500 || Input.Content.Length > 500 || Input.Title.Length <= 0 ||
                Input.Content.Length <= 0)
            {
                _logger.LogError("Title and Content must be between 1 and 500 characters");
                RedirectToPage("/Error");
            }

            else
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                Thread = new DiscussionThread
                {
                    Title = Input.Title,
                    Content = Input.Content,
                    ApplicationUserId = userId,
                    CreatedAt = DateTimeOffset.UtcNow,
                    ForumId = forumId
                };

                ThreadService.AddAsync(Thread);
            }
        }

        public class InputModel
        {
            public string Title { get; set; }
            public string Content { get; set; }
        }

    }
}
