using System.Security.Claims;
using DiscussionBoard.Models;
using DiscussionBoard.Services;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public Create(IDiscussionThreadService _threadService, UserManager<ApplicationUser> userManager, ILogger<Create> logger)
        {
            _logger = logger;
            ThreadService = _threadService;
            _userManager = userManager;
            Input = new InputModel();
        }

        public async Task<IActionResult> OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            return Page();
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
                Thread = new DiscussionThread()
                {
                    Title = Input.Title,
                    Content = Input.Content,
                    ApplicationUserId = _userManager.GetUserId(User),
                    ApplicationUser = _userManager.GetUserAsync(User).GetAwaiter().GetResult(),
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
